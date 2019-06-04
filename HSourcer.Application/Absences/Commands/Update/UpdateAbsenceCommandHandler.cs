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

namespace HSourcer.Application.Absences.Commands.Update
{
    public class UpdateAbsenceCommandHandler : IRequestHandler<UpdateAbsenceCommand, int>
    {
        private readonly IHSourcerDbContext _context;
        private readonly UserResolverService _userResolver;

        public UpdateAbsenceCommandHandler(IHSourcerDbContext context, UserResolverService userResolver)
        {
            _context = context;
            _userResolver = userResolver;
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

            return entity.AbsenceId;
        }
    }
}