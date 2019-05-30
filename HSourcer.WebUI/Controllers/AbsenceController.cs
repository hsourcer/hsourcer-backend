using AutoMapper;
using HSourcer.Application.Absences.Commands.Create;
using HSourcer.Application.Absences.Commands.Update;
using HSourcer.Application.Absences.Queries;
using HSourcer.WebUI.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HSourcer.WebUI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    public class AbsenceController : BaseController
    {
        public AbsenceController(IMapper mapper) : base(mapper) { }

        ///<summary>
        ///Creation of the absence.
        ///</summary>
        ///<remarks>
        ///Restrictions:
        ///Contract person cannot be the one that submits the absence.
        ///</remarks>
        [AllowAnonymous]
        [HttpPost]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        public async Task<ActionResult> Create([FromBody] CreateAbsenceCommand command)
        {
            var absenceId = await Mediator.Send(command);
            return Created("Created Absence", absenceId);
        }
        ///<summary>
        ///Acceptance or rejection of the absence.
        ///</summary>
        ///<remarks>
        ///Restrictions:
        ///* can be used only by TEAM_LEADER //should be filtred in authorize
        ///* status must be either accept/reject
        ///* absenceId must refer to absence posted by the user within the same team
        ///</remarks>
        [HttpPut]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Update([FromBody] UpdateAbsenceCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }
        ///<summary>
        ///Produces list of absences for user's team, within specified dates.
        ///</summary>
        ///<remarks>
        ///Remarks:
        ///* any user can access his team's absences.
        ///
        ///Restrictions:
        ///* produced result is only for the team, not the organization.
        ///</remarks>
        [HttpGet]
        [Authorize]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(IEnumerable<AbsenceViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAbsence([FromQuery] GetAbsenceQuery query)
        {
            var queryResult = await Mediator.Send(query);
            var x = HttpContext.User;
            var displayResult = _mapper.Map(queryResult, typeof(IEnumerable<AbsenceModel>), typeof(IEnumerable<AbsenceViewModel>));

            return Ok(displayResult);
        }
    }
}
