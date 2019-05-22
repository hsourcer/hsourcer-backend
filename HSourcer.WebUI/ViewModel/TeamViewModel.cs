using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using HSourcer.Application.Interfaces.Mapping;
using HSourcer.Application.Teams.Queries;
using HSourcer.Domain.Entities;
using HSourcer.Domain.Enums;
using HSourcer.Domain.Security;

namespace HSourcer.WebUI.ViewModels
{
    public class TeamViewModel : IMapFrom<TeamModel>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<UserViewModel> TeamLeaders { get; set; }
        public IEnumerable<UserViewModel> Users { get; set; }
    }
}