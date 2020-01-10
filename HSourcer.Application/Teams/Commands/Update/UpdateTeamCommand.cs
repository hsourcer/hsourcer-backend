using MediatR;
using System;
using HSourcer.Domain.Enums;
using System.Collections.Generic;

namespace HSourcer.Application.Absences.Commands.Create
{
    public class UpdateTeamCommand : IRequest<int>
    {
        public int TeamId { get; set; }
        public int TeamLeader { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<int> Users { get; set; }
    }
}