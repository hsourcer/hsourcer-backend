using MediatR;
using System;
using HSourcer.Domain.Enums;

namespace HSourcer.Application.Absences.Commands.Update
{
    public class UpdateOrganizationCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}