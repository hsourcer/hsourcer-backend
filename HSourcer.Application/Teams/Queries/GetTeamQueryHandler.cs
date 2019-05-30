using System.Threading;
using System.Threading.Tasks;
using MediatR;
using HSourcer.Application.Exceptions;
using HSourcer.Application.Interfaces;
using HSourcer.Domain.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HSourcer.Application.Teams.Queries
{
    public class GetAbsenceQueryHandler : IRequestHandler<GetTeamQuery, IEnumerable<TeamModel>>
    {
        private readonly IHSourcerDbContext _context;

        public GetAbsenceQueryHandler(IHSourcerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TeamModel>> Handle(GetTeamQuery request, CancellationToken cancellationToken)
        {
            //type problem
            IQueryable<Team> query = _context.Teams
                .Include(u => u.Users)
                    .ThenInclude(r => r.UserRoles);

            //TODO identity
            var thisUserOrganization = 1;
            //join on user within the organization
            query = query.Where(t => t.OrganizationId == thisUserOrganization);

            var entities = await query.ToListAsync();

            if (entities == null)
            {
                entities = new List<Team>();
            }
            var x = entities.Select(e => TeamModel.Create(e));
            return x;
        }
    }
}




