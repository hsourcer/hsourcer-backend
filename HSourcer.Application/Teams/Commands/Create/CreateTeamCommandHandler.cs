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
using Microsoft.AspNetCore.Identity;

namespace HSourcer.Application.Absences.Commands.Create
{
    public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, int>
    {
        private readonly IHSourcerDbContext _context;
        private readonly IUserResolve _userResolver;
        private readonly UserManager<User> _userManager;


        public CreateTeamCommandHandler(IHSourcerDbContext context, IUserResolve userResolver, UserManager<User> userManager)
        {
            _context = context;
            _userResolver = userResolver;
            _userManager = userManager;
        }

        public async Task<int> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            var user = await _userResolver.GetUserIdentity();
            var teams = await _context.Teams.Where(t => t.Organization.Teams.Any(w => w.TeamId == user.TeamId)).Select(w => w.TeamId).ToListAsync();


            var organization = await _context.Organizations.FirstAsync(o => o.Teams.Any(t => t.TeamId == user.TeamId));

            var entity = new Team
            {
                Name = request.Name,
                OrganizationId = organization.OrganizationId,
                Description = request.Description,
                CreationDate = DateTime.UtcNow,
                CreatedBy = user.Id
            };

            if (request.Users != null)
            {
                var usersToUpdate = await _context.Users.Where(u => request.Users.Contains(u.Id) && teams.Contains(u.TeamId)).ToListAsync();
                entity.Users = usersToUpdate;

                if (request.TeamLeader != 0)
                {
                    var tl = usersToUpdate.FirstOrDefault(u => u.Id == request.TeamLeader);

                    if (tl != null && tl.UserRole == "EMPLOYEE")
                    {
                        tl.UserRole = "TEAM_LEADER";
                        await _userManager.AddToRoleAsync(tl, Enum.GetName(typeof(RoleEnum), RoleEnum.TEAM_LEADER));
                        await _userManager.RemoveFromRoleAsync(tl, Enum.GetName(typeof(RoleEnum), RoleEnum.EMPLOYEE));
                    }
                }

            }

            _context.Teams.Add(entity);

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                throw new Exception("Save changes failed in databse" + e.ToString());
            };

            return entity.TeamId;
        }
    }
}