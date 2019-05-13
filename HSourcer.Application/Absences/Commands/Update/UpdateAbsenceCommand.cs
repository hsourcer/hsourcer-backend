using MediatR;
using System;
using HSourcer.Domain.Enums;

namespace HSourcer.Application.Absences.Commands.Update
{
    public class UpdateAbsenceCommand : IRequest<int>
    {
        public StatusEnum Status { get; set; }
        public string Comment { get; set; }
        public int AbsenceId { get; set; }
    }
}