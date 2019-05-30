using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HSourcer.Application.Absences.Commands.Update
{
    public class UpdateAbsenceCommandValidator : AbstractValidator<UpdateAbsenceCommand>
    {
        public UpdateAbsenceCommandValidator()
        {
            ///* status must be either accept/reject
            ///* absenceId must refer to absence posted by the user within the same team
            RuleFor(v => true);
        }
    }
}
