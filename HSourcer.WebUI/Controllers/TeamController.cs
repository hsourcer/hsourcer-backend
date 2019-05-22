using AutoMapper;
using HSourcer.Application.Teams.Queries;
using HSourcer.WebUI.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HSourcer.WebUI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    public class TeamController : BaseController
    {
        public TeamController(IMapper mapper) : base(mapper) { }
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
