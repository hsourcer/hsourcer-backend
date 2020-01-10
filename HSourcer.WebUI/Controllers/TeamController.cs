using AutoMapper;
using HSourcer.Application.Absences.Commands.Create;
using HSourcer.Application.Teams.Queries;
using HSourcer.Application.UserIdentity;
using HSourcer.WebUI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HSourcer.WebUI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]

    public class TeamController : BaseController
    {
        private IUserResolve _us;
        public TeamController(IMapper mapper, IUserResolve us) : base(mapper) {
            _us = us;
        }

        ///<summary>
        ///Produces list of all teams and theirs members.
        ///</summary>
        ///<remarks>
        ///Remarks:
        ///* any user can access information about the teams,
        ///* there is only 1 TEAM_LEADER.
        ///
        ///Restrictions:
        ///* produced result is only for the teams within the user's organization.
        ///</remarks>
        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(IEnumerable<TeamViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetTeams([FromQuery] GetTeamQuery query)
        {
            var queryResult = await Mediator.Send(query);

            var displayResult = _mapper.Map(queryResult, typeof(IEnumerable<TeamModel>), typeof(IEnumerable<TeamViewModel>));

            return Ok(displayResult);
        }
        ///<summary>
        ///Creates new team, assigns users to it.
        ///</summary>
        [HttpPost]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> CreateTeam([FromBody] CreateTeamCommand command)
        {
            var user = await _us.GetUserIdentity();
            if (user.UserRole != "ADMIN")
                return Unauthorized();

            var result = await Mediator.Send(command);
            return Ok(result);
        }
        ///<summary>
        ///Updates team, assigns users to it.
        ///</summary>
        [HttpPut]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateTeam([FromBody] UpdateTeamCommand command)
        {

            var user = await _us.GetUserIdentity();
            if (user.UserRole != "ADMIN")
                return Unauthorized();

            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}
