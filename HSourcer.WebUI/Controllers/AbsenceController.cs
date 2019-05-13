using HSourcer.Application.Absences.Commands.Create;
using HSourcer.Application.Absences.Commands.Update;
using HSourcer.Application.Absences.Queries;
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
    public class AbsenceController : BaseController
    {
        [HttpPost]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> Create([FromBody] CreateAbsenceCommand command)
        {
            var absenceId = await Mediator.Send(command);

            return CreatedAtAction("Created Absence", absenceId);
        }
        [HttpPut]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Update([FromBody] UpdateAbsenceCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }
        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAbsences([FromQuery] GetAbsencesQuery query)
        {
            var absences = await Mediator.Send(query);

            return Ok(absences);
        }



    }
}
