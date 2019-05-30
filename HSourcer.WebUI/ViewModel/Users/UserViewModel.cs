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
        public int Id { get; set; }
        public bool Active { get; set; }
        public int TeamId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PhotoPath { get; set; }
        public string UserRole { get; set; }
    }

    public class ExtendedUserViewModel : UserViewModel
    {
        public ExtendedUserViewModel(UserViewModel userViewModel)
        {
            foreach (var prop in typeof(UserViewModel).GetProperties())
            {
                this.GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(userViewModel, null), null);
            }
        }

        public string UserToken { get; set; }
        //TODO, make this into extension?
        //public void CreateMappings(Profile configuration)
        //{
        //    configuration.CreateMap<User, UserViewModel>()
        //        .ForMember(w => w.UserToken, opt => opt.Ignore())
        //        .ForMember(w => w.UserRole, opt => opt.Ignore());

        //    //(typeof(User), typeof(UserViewModel),)
        //    //.ForMember("UserToken",m=>m.Ignore())
        //    //.ForMember("UserRole", opt => opt.Ignore());
        //}
    }

}