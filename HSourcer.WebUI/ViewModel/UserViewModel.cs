using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using HSourcer.Application.Interfaces.Mapping;
using HSourcer.Domain.Entities;
using HSourcer.Domain.Enums;
using HSourcer.Domain.Security;

namespace HSourcer.WebUI.ViewModels
{
    public class UserViewModel : IMapFrom<User>
    {
        public int UserId { get; set; }
        public bool Active { get; set; }
        public int TeamId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string PhotoPath { get; set; }
        public int Role { get; set; } //TOOD enum + in

    }
}