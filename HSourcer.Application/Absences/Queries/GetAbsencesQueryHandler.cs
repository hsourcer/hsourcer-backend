using System.Threading;
using System.Threading.Tasks;
using MediatR;
using HSourcer.Application.Exceptions;
using HSourcer.Application.Interfaces;
using HSourcer.Domain.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HSourcer.Application.Absences.Queries
{
    public class GetAbsencesQueryHandler : IRequestHandler<GetAbsencesQuery, IEnumerable<AbsencesModel>>
    {
        private readonly IHSourcerDbContext _context;

        public GetAbsencesQueryHandler(IHSourcerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AbsencesModel>> Handle(GetAbsencesQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Absences
                .Where(a =>
                a.StartDate >= request.StartDate
                && a.EndDate <= request.EndDate);

            //TODO identity
            var thisUserTeamId = 0;
            //join on user within the team
            query = query.Where(w => w.User.TeamId == thisUserTeamId);

            var entities = await query.ToListAsync();

            if (entities == null)
            {
                entities = new List<Absence>();
            }

            return entities.Select(e => AbsencesModel.Create(e));
        }
    }
}