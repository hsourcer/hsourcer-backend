using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using HSourcer.Domain.Entities;
using HSourcer.Domain.Enums;
using HSourcer.Domain.Security;

namespace HSourcer.Application.Teams.Queries
{
    public class TeamModel
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public User TeamLeader { get; set; }
        public IEnumerable<User> Users { get; set; }
        public static Expression<Func<Team, TeamModel>> Projection
        {
            get
            {
                return team => new TeamModel
                {
                    TeamId = team.TeamId,
                    Name = team.Name,
                    TeamLeader = team.Users.FirstOrDefault(w => w.UserRoles.Any(r=>r.RoleId == (int)RoleEnum.TEAM_LEADER)),
                    Users = team.Users
                };
            }
        }

        public static TeamModel Create(Team team)
        {
            return Projection.Compile().Invoke(team);
        }
    }
}