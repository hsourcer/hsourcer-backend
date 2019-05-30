using HSourcer.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HSourcer.Application.UserIdentity
{
    public class UserResolverService
    {
        private readonly IHttpContextAccessor _context;
        private readonly UserManager<User> _userManager;
        public UserResolverService(IHttpContextAccessor context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<User> GetUserIdentity()
        {
            var identity = _context.HttpContext.User;

            if (identity == null)
                throw new Exception("User not authorized for the context.");

            var user =await _userManager.GetUserAsync(identity);

            if (user==null)
                throw new Exception("User not authorized for the context.");

            return user;
        }
    }
}
