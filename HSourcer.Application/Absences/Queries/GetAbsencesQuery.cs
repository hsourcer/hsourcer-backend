using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HSourcer.Application.Absences.Queries
{
    public class GetAbsencesQuery : IRequest<IEnumerable<AbsencesModel>>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
