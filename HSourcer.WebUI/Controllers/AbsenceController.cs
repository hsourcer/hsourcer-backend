using AutoMapper;
using HSourcer.Application.Absences.Commands.Create;
using HSourcer.Application.Absences.Commands.Update;
using HSourcer.Application.Absences.Queries;
using HSourcer.WebUI.ViewModels;
using MediatR;
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
        [ProducesResponseType(typeof(IEnumerable<AbsenceViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAbsence([FromQuery] GetAbsenceQuery query)
        {
            var queryResult = await Mediator.Send(query);

            var displayResult = _mapper.Map(queryResult, typeof(IEnumerable<AbsenceModel>), typeof(IEnumerable<AbsenceViewModel>));

            return Ok(displayResult);
        }
    }
}
