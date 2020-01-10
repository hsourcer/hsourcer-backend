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
    public class UpdateTeamCommandHandler : IRequestHandler<UpdateTeamCommand, int>
    {
        private readonly IHSourcerDbContext _context;
        private readonly IUserResolve _userResolver;
        private readonly UserManager<User> _userManager;

        public UpdateTeamCommandHandler(IHSourcerDbContext context, IUserResolve userResolver, UserManager<User> userManager)
        {
            _context = context;
            _userResolver = userResolver;
            _userManager = userManager;
        }

        public async Task<int> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
        {
            var user = await _userResolver.GetUserIdentity();
            var teams = await _context.Teams.Where(t => t.Organization.Teams.Any(w => w.TeamId == user.TeamId)).Select(w=>w.TeamId).ToListAsync();
            var team = await _context.Teams.FirstOrDefaultAsync(t => t.TeamId == request.TeamId && teams.Contains(t.TeamId));

            if (team == null)
                throw new Exception("TeamId is not within user's organization or does not exist.");

            team.Name = request.Name;
            team.Description = request.Description;

            if (request.Users != null)
            {
                var usersToUpdate = await _context.Users.Where(u => request.Users.Contains(u.Id)
                && teams.Contains(team.TeamId)&& u.TeamId != team.TeamId).ToListAsync();
                foreach(var u in usersToUpdate)
                    team.Users.Add(u);

                if (request.TeamLeader != 0)
                {
                    var tl = usersToUpdate.FirstOrDefault(u => u.Id == request.TeamLeader);
                    if (tl != null && tl.UserRole == "EMPLOYEE" && !team.Users.Any(w=>w.UserRole == "TEAM_LEADER"))
                    {
                        tl.UserRole = "TEAM_LEADER";
                        await _userManager.AddToRoleAsync(tl, Enum.GetName(typeof(RoleEnum), RoleEnum.TEAM_LEADER));
                        await _userManager.RemoveFromRoleAsync(tl, Enum.GetName(typeof(RoleEnum), RoleEnum.EMPLOYEE));
                    }
                }

            }
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                throw new Exception("Save changes failed in databse" + e.ToString());
            };

            return team.TeamId;
        }
    }
}