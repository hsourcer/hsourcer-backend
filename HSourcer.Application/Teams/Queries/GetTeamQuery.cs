using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HSourcer.Application.Teams.Queries
{
    public class GetTeamQuery : IRequest<IEnumerable<TeamModel>>
    {
        //no parameters for now
    }
}
