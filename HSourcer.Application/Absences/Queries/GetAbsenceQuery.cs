using HSourcer.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HSourcer.Application.Absences.Queries
{
    public class GetAbsenceQuery : IRequest<IEnumerable<AbsenceModel>>
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public AbsenceEnum? AbsenceType { get; set; }
        //public StatusEnum? Status { get; set; }
    }
}
