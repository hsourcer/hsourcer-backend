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
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<User> TeamLeaders { get; set; }
        public IEnumerable<User> Users { get; set; }
        public static Expression<Func<Team, TeamModel>> Projection
        {
            get
            {
                return team => new TeamModel
                {
                    Id = team.TeamId,
                    Name = team.Name,
                    //for now like this, later we will use enum casting
                    TeamLeaders =team.Users.Where(w=>w.Role == (int)RoleEnum.TEAM_LEADER),
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