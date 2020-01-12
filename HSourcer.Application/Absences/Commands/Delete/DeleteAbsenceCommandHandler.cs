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
using HSourcer.Domain.Enums;

namespace HSourcer.Application.Absences.Commands
{
    public class DeleteAbsenceCommandHandler : IRequestHandler<DeleteAbsenceCommand, int>
    {
        private readonly IHSourcerDbContext _context;
        private readonly IUserResolve _userResolver;

        public DeleteAbsenceCommandHandler(IHSourcerDbContext context, IUserResolve userResolver)
        {
            _context = context;
            _userResolver = userResolver;
        }

        public async Task<int> Handle(DeleteAbsenceCommand request, CancellationToken cancellationToken)
        {
            var user = await _userResolver.GetUserIdentity();

            var absence = await _context.Absences.FirstOrDefaultAsync(a => a.AbsenceId == request.AbsenceId && a.User.Id == user.Id);

            if (absence == null)
                throw new Exception("Absence does not exists, or you have no permission to delete it.");

            absence.Status = (int)StatusEnum.DELETED;
            absence.DecisionDate = DateTime.Now;

            _context.Absences.Update(absence);
            await _context.SaveChangesAsync(cancellationToken);

            return absence.AbsenceId;
        }
    }
}