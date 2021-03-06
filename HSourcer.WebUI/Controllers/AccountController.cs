﻿using AutoMapper;
using HSourcer.Application.UserIdentity;
using HSourcer.Application.Users.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HSourcer.WebUI.Controllers
{
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    public class AccountController : BaseController
    {
        IUserResolve _userResolver;
        public AccountController(IMapper mapper, IUserResolve userResolver) : base(mapper)
        {
            _userResolver = userResolver;
        }

        ///<summary>
        ///Creation of the user.
        ///</summary>
        ///<remarks>
        ///Restrictions:
        ///* can be used only by TEAM_LEADER and ADMIN //should be filtred in authorize
        ///* unique email
        ///* if submitted by TEAM_LEADER, teamId must be equal to his
        ///* ADMIN can create both roles, TEAM_LEADER 
        ///</remarks>
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        public async Task<ActionResult> Create([FromBody] CreateUserCommand command)
        {
            var user = await _userResolver.GetUserIdentity();
            if (user.UserRole != "ADMIN")
                return Unauthorized();

            var userId = await Mediator.Send(command);
            return Created("User Created.", userId);
        }
        ///<summary>
        ///Update of the user.
        ///</summary>
        ///<remarks>
        ///Restrictions:
        ///* can be used only by TEAM_LEADER and ADMIN //should be filtred in authorize
        ///* unique email, after the change,
        ///* teamId cannot be changed,
        ///* user must be from the TEAM_LEADER's team or change must be done by ADMIN.
        ///</remarks>
        [HttpPost]
        [Route("update")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult> Update([FromBody] UpdateUserCommand command)
        {
            var userId = await Mediator.Send(command);
            return Ok(userId);
        }
        ///<summary>
        ///Deletion (Deactivation) of the user.
        ///</summary>
        ///<remarks>
        ///Restrictions:
        ///* can be used only by TEAM_LEADER and ADMIN //should be filtred in authorize
        ///* user must be from the TEAM_LEADER's team or change must be done by ADMIN.
        ///</remarks>
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [Route("delete")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromBody] DeleteUserCommand command)
        {

            var user = await _userResolver.GetUserIdentity();
            if (user.UserRole != "ADMIN")
                return Unauthorized();

            var userId = await Mediator.Send(command);
            return Ok(userId);
        }
    }
}