using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HSourcer.Application.Teams.Queries
{
    public class GetTeamQueryValidator : AbstractValidator<GetTeamQuery>
    {
        public GetTeamQueryValidator()
        {
            //get identity?
            //validation on properties not needed
            RuleFor(v => true);
        }
    }
}
