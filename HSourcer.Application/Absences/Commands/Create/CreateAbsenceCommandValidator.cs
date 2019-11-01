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
           // RuleFor(v => v.StartDate < v.EndDate);
           // RuleFor(v => v.ContactPersonId > 0);
        }
    }
}
