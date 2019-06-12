using AutoMapper;
using HSourcer.Application.Teams.Queries;
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
        public TeamController(IMapper mapper) : base(mapper) { }

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
    }
}
