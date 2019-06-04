using System.Threading;
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

            //get team leader + check if exists
            var teamLeader = await _context.Users.FirstOrDefaultAsync(w => w.TeamId == user.TeamId);
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

            _context.Absences.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            var message = new Message
            {
                Subject = "User creation",
                Body = "User created :)",
                To = new List<string> { user.Email }
            };
            await _notificationService.SendAsync(message);

            return entity.AbsenceId;
        }
    }
}