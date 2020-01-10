using AutoMapper;
using HSourcer.Application.Absences.Commands.Create;
using HSourcer.Application.Absences.Commands.Update;
using HSourcer.Application.Absences.Queries;
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

    public class OrganizationController : BaseController
    {
        private IUserResolve _us;
        public OrganizationController(IMapper mapper, IUserResolve us) : base(mapper)
        {
            _us = us;
        }
        ///<summary>
        ///Acceptance or rejection of the absence.
        ///</summary>
        [HttpPut]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Update([FromBody] UpdateOrganizationCommand command)
        {
            var user = await _us.GetUserIdentity();
            if (user.UserRole != "ADMIN")
                return Unauthorized();

            await Mediator.Send(command);
            return Ok();
        }
    }
}
