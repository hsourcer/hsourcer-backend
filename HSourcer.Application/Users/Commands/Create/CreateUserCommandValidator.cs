using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HSourcer.Application.Users.Commands.Create
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            ///Unique email, should it be changable?
            //User must be from the teamLeader team or operation must be done by the Admin
            //ADMIN can create both roles, TEAM_LEADER 
            RuleFor(v => true);
        }
    }
}
