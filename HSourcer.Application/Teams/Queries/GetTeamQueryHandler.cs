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

namespace HSourcer.Application.Teams.Queries
{
    public class GetAbsenceQueryHandler : IRequestHandler<GetTeamQuery, IEnumerable<TeamModel>>
    {
        private readonly IHSourcerDbContext _context;
        private readonly UserResolverService _userResolver;

        public GetAbsenceQueryHandler(IHSourcerDbContext context, UserResolverService userResolver)
        {
            _context = context;
            _userResolver = userResolver;
        }

        public async Task<IEnumerable<TeamModel>> Handle(GetTeamQuery request, CancellationToken cancellationToken)
        {
            //type problem
            IQueryable<Team> query = _context.Teams
                .Include(u => u.Users);

            var user = (await _userResolver.GetUserIdentity());
            var organization = await _context.Users
                .Where(u => u == user)
                .Include(w => w.Team.Organization)
                .Select(w => w.Team.Organization).FirstAsync();
            //join on user within the organization
            query = query.Where(t => t.Organization == organization);

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




