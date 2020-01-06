using System.Threading;
using System.Threading.Tasks;
using MediatR;
using HSourcer.Application.Exceptions;
using HSourcer.Application.Interfaces;
using HSourcer.Domain.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using HSourcer.Application.UserIdentity;
using HSourcer.Domain.Security;

namespace HSourcer.Application.Absences.Queries
{
    public class GetAbsenceQueryHandler : IRequestHandler<GetAbsenceQuery, IEnumerable<AbsenceModel>>
    {
        private readonly IHSourcerDbContext _context;
        private readonly UserResolverService _userResolver;

        public GetAbsenceQueryHandler(IHSourcerDbContext context, UserResolverService userResolver)
        {
            _context = context;
            _userResolver= userResolver;
        }

        public async Task<IEnumerable<AbsenceModel>> Handle(GetAbsenceQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Absence> query =  _context.Absences;
            var user = await _userResolver.GetUserIdentity();

            #region Access logic
            if(user.UserRole == RoleEnum.ADMIN.ToString())
            {
                var teams =await _context.Teams.Where(t => t.Organization == user.Team.Organization).Select(t=>t.TeamId).ToListAsync();
                query = query.Where(w => teams.Contains(w.User.TeamId));
            }
            else    
                query = query.Where(w => w.User.TeamId == user.TeamId);
            #endregion

            #region Business logic
            query = query
                .Where(a =>
                (request.StartDate == null ||  a.StartDate >= request.StartDate)
                &&
                (request.EndDate == null || a.EndDate <= request.EndDate)
                &&
                (request.AbsenceType == null || a.TypeId == (int)request.AbsenceType) 
                // &&
                // (request.Status == null || a.TypeId == (int)request.Status)
                );
            #endregion

            var entities = await query.ToListAsync();

            return entities.Select(e => AbsenceModel.Create(e));
        }
    }
}