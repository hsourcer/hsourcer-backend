using MediatR;
using System;
using HSourcer.Domain.Enums;

namespace HSourcer.Application.Users.Commands
{
    public class DeleteUserCommand : IRequest<int>
    {
        public int UserId { get; set; }
    }
}