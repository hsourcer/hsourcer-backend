using AutoMapper;
using HSourcer.Domain.Entities;
using HSourcer.WebUI.Auth;
using HSourcer.WebUI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HSourcer.WebUI.Controllers
{
    [ApiController]
    public class AuthController : BaseController
    {
        public AuthController(IOptions<TokenConfig> tokenConfig, IMapper mapper, SignInManager<User> _signInManager, UserManager<User> manager) : base(mapper) {
            TokenConfigOptions = tokenConfig;
            this._signInManager = _signInManager;
            this._manager = manager;
        }

        readonly SignInManager<User> _signInManager;
        readonly UserManager<User> _manager;

        public IOptions<TokenConfig> TokenConfigOptions { get; }

        [HttpPost]
        [Route("/login/")]
        [MapToApiVersion("1.0")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ExtendedUserViewModel),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Login(string id, string password)
        {
            var logIn = await _signInManager.PasswordSignInAsync(id,password,false,false);
            if (logIn.Succeeded)
            {
                var user = await _manager.FindByEmailAsync(id);
               
                var tokenHandler = new CustomTokenHandler(user, TokenConfigOptions);

                var displayResult =new ExtendedUserViewModel(_mapper.Map(user, typeof(User), typeof(UserViewModel)) as UserViewModel);

                displayResult.UserToken = tokenHandler.GenerateToken();

                return Ok(displayResult);
            }
            return NotFound();
        }
        //Log out
        [HttpPost]
        [Route("/logout/")]
        [MapToApiVersion("1.0")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> LoginOut()
        {
            return Ok();
        }
        [HttpPost]
        [Route("/submitPassword/")]
        [MapToApiVersion("1.0")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> SubmitPassword()
        {
            return Ok();
        }
        //Postpasword
        //reset pasword
    }
}