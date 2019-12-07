﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using HSourcer.Application.Interfaces;
using HSourcer.Domain.Entities;
using System;
using HSourcer.Domain.Security;
using HSourcer.Application.UserIdentity;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HSourcer.Application.Notifications.Models;
using System.Collections.Generic;

namespace HSourcer.Application.Absences.Commands.Create
{
    public class CreateAbsenceCommandHandler : IRequestHandler<CreateAbsenceCommand, int>
    {
        private readonly IHSourcerDbContext _context;
        private readonly UserResolverService _userResolver;
        private readonly INotificationService _notificationService;

        public CreateAbsenceCommandHandler(IHSourcerDbContext context, UserResolverService userResolver, INotificationService notificationService)
        {
            _context = context;
            _userResolver = userResolver;
            _notificationService = notificationService;
        }

        public async Task<int> Handle(CreateAbsenceCommand request, CancellationToken cancellationToken)
        {
            var user = await _userResolver.GetUserIdentity();
          
            var teamLeader = await _context.Users.FirstOrDefaultAsync(w => w.TeamId == user.TeamId && w.UserRole == RoleEnum.TEAM_LEADER.ToString());
            if (teamLeader == null)
            {
                throw new Exception("There is no teamLeader for this user!");
            }
         
            var entity = new Absence
            {
                UserId = user.Id,
                ContactPersonId = request.ContactPersonId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                CreationDate = DateTime.UtcNow,
                TypeId = (int)request.AbsenceType,
                TeamLeaderId = teamLeader.Id
            };

            try
            {
                _context.Absences.Add(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                throw new Exception("something failed in databse" + e.ToString());
            };

            //To be changed
            entity = await _context.Absences.Where(a => a == entity).Include(w=>w.ContactPerson).Include(u=>u.User).FirstAsync();
            var body = await _notificationService.RenderViewToStringAsync("Notification", entity);

            var message = new Message
            {
                Subject = "Masz nowe zgłoszenie w Hsourcer",
                MimeType = "Html",
                Body = body,
                To = new List<string> { teamLeader.Email }
            };
            await _notificationService.SendAsync(message);

            return entity.AbsenceId;
        }
    }
}