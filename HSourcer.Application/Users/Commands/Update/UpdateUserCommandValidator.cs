using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HSourcer.Application.Users.Commands.Update
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            //User must be from the teamLeader team or operation must be done by the Admin
            ///Unique email
            RuleFor(v => true);
        }
    }
}
