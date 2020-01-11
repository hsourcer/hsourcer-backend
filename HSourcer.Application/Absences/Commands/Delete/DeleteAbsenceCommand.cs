using MediatR;
using System;
using HSourcer.Domain.Enums;

namespace HSourcer.Application.Absences.Commands
{
    public class DeleteAbsenceCommand : IRequest<int>
    { 
        public int AbsenceId { get; set; }
    }
}