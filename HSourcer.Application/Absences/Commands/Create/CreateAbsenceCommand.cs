using MediatR;
using System;
using HSourcer.Domain.Enums;

namespace HSourcer.Application.Absences.Commands.Create
{
    public class CreateAbsenceCommand : IRequest<int>
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int? ContactPersonId { get; set; }

        public AbsenceEnum AbsenceType { get; set; }
    }
}