using MediatR;
using System;
using HSourcer.Domain.Enums;
using System.Collections.Generic;

namespace HSourcer.Application.Absences.Commands.Create
{
    public class CreateTeamCommand : IRequest<int>
    {
        public string Name { get; set; }
        public int TeamLeader { get; set; }
        public string Description { get; set; }
        public List<int> Users { get; set; }

    }
}