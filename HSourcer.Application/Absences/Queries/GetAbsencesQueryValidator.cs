using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HSourcer.Application.Absences.Queries
{
    public class GetAbsencesQueryValidator : AbstractValidator<GetAbsencesQuery>
    {
        public GetAbsencesQueryValidator()
        {
            //get identity?
            //validation on properties not needed
            RuleFor(v => true);
        }
    }
}
