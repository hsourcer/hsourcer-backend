using MediatR;
using System;
using HSourcer.Domain.Enums;
using HSourcer.Domain.Security;

namespace HSourcer.Application.Users.Commands
{
    public class CreateUserCommand : IRequest<int>
    {
        public int TeamId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public RoleEnum UserRole { get; set; }
    }
}