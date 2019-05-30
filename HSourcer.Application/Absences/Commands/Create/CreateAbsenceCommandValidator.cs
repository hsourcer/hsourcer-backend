using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HSourcer.Application.Absences.Commands.Create
{
    public class CreateAbsenceCommandValidator : AbstractValidator<CreateAbsenceCommand>
    {
        public CreateAbsenceCommandValidator()
        {
            ///Contract person cannot be the one that submits the absence.
            RuleFor(v => true);
        }
    }
}
