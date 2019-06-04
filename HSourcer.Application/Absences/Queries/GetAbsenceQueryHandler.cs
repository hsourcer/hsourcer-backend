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
            var query = _context.Absences
                .Where(a =>
                a.StartDate >= request.StartDate
                && a.EndDate <= request.EndDate);

            var user = await _userResolver.GetUserIdentity();

            query = query.Where(w => w.User.TeamId == user.TeamId);

            var entities = await query.ToListAsync();

            if (entities == null)
            {
                entities = new List<Absence>();
            }

            return entities.Select(e => AbsenceModel.Create(e));
        }
    }
}