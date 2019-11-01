﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using HSourcer.Application.Interfaces;
using HSourcer.Domain.Entities;
using System;
using HSourcer.Application.UserIdentity;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HSourcer.Domain.Security;
using HSourcer.Application.Notifications.Models;
using System.Collections.Generic;

namespace HSourcer.Application.Absences.Commands.Update
{
    public class UpdateAbsenceCommandHandler : IRequestHandler<UpdateAbsenceCommand, int>
    {
        private readonly IHSourcerDbContext _context;
        private readonly UserResolverService _userResolver;
        private readonly INotificationService _notificationService;

        public UpdateAbsenceCommandHandler(IHSourcerDbContext context, UserResolverService userResolver, INotificationService notificationService)
        {
            _context = context;
            _userResolver = userResolver;
            _notificationService = notificationService;
        }

        public async Task<int> Handle(UpdateAbsenceCommand request, CancellationToken cancellationToken)
        {
            var query = from absences in _context.Absences
                        where absences.AbsenceId == request.AbsenceId
                        select absences;

            query = query.Include(a => a.User);

            var entity = await query.FirstOrDefaultAsync();

            var user = await _userResolver.GetUserIdentity();

            if (user.TeamId != entity.User.TeamId)
                throw new Exception("User cannot accept absence from another team.");

            if (user.UserRole != Enum.GetName(typeof(RoleEnum),RoleEnum.TEAM_LEADER))
                throw new Exception("Only Team Leader can accept or decline absence.");

            entity.Status = (int)request.Status;
            entity.DecisionDate = DateTime.UtcNow;
            entity.Comment = request.Comment;
            entity.TeamLeaderId = user.Id;


            _context.Absences.Update(entity);

            await _context.SaveChangesAsync(cancellationToken);
    
            var message = new Message
            {
                Subject = "New Absence",
                Body = "Please accept/reject absence for " + user.FirstName + " " + user.LastName + ", thank you!",
                To = new List<string> { entity.User.Email }
            };
            await _notificationService.SendAsync(message);


            return entity.AbsenceId;
        }
    }
}