using MediatR;
using System;
using HSourcer.Domain.Enums;

namespace HSourcer.Application.Users.Commands
{
    public class UpdateUserCommand : IRequest<int>
    {
        public int UserId { get; set; }
        public bool Active { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}