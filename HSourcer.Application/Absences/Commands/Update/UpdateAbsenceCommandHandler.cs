using System.Threading;
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
        private readonly IUserResolve _userResolver;
        private readonly INotificationService _notificationService;

        public UpdateAbsenceCommandHandler(IHSourcerDbContext context, IUserResolve userResolver, INotificationService notificationService)
        {
            _context = context;
            _userResolver = userResolver;
            _notificationService = notificationService;
        }

        public async Task<int> Handle(UpdateAbsenceCommand request, CancellationToken cancellationToken)
        {
            var query = from absences in _context.Absences
                        .Include(c=>c.ContactPerson)
                        .Include(u=>u.User)
                        where absences.AbsenceId == request.AbsenceId
                        select absences;
            
            var entity = await query.FirstOrDefaultAsync();
            var user = await _userResolver.GetUserIdentity();

            var teams = await _context.Teams.Where(t => t.Organization.Teams.Any(o => o.TeamId == user.TeamId)).Select(t => t.TeamId).ToListAsync();

            if (!teams.Contains(user.TeamId))
                throw new Exception("User must be in the same organization.");

            if (user.UserRole== Enum.GetName(typeof(RoleEnum),RoleEnum.EMPLOYEE))
                throw new Exception("Only Team Leader or Admin can accept or decline absence.");

            entity.Status = (int)request.Status;
            entity.DecisionDate = DateTime.UtcNow;
            entity.Comment = request.Comment;
            entity.TeamLeaderId = user.Id;


            _context.Absences.Update(entity);

            await _context.SaveChangesAsync(cancellationToken);

            var viewName = "Accept";
            var subject = "Twój wniosek w Hsourcer został zaakceptowany";
            if (request.Status != Domain.Enums.StatusEnum.ACCEPTED)
            {
                viewName = "Reject";
                subject = " Twój wniosek w Hsourcer został odrzucony";
            }
           
            var body = await _notificationService.RenderViewToStringAsync(viewName, entity);

            var message = new Message
            {
                Subject = subject,
                MimeType = "Html",
                Body = body,
                // To = new List<string> { entity.User.Email }
                To = new List<string> { "user@hscr.site" }
            };
            await _notificationService.SendAsync(message);


            return entity.AbsenceId;
        }
    }
}