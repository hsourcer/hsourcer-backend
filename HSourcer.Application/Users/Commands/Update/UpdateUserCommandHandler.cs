 using System.Threading;
using System.Threading.Tasks;
using MediatR;
using HSourcer.Application.Interfaces;
using HSourcer.Domain.Entities;
using System;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using HSourcer.Application.Exceptions;
using HSourcer.Application.UserIdentity;

namespace HSourcer.Application.Users.Commands
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, int>
    {
        private readonly IHSourcerDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly UserResolverService _userResolver;

        public UpdateUserCommandHandler(IHSourcerDbContext context, UserManager<User> userManager, UserResolverService userResolver)
        {
            _context = context;
            _userManager = userManager;
            _userResolver = userResolver;
        }

        public async Task<int> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {

            var existingUser = await _userManager.FindByIdAsync(request.UserId.ToString());

            var currentUser = await _userResolver.GetUserIdentity();
            if (!(currentUser.UserRole == "ADMIN") && existingUser.Id != currentUser.Id)
                throw new Exception("User is not allowed to edit other users.");

            if (existingUser == null)
                throw new NotFoundException("User", request.UserId);

            existingUser.Active = request.Active;
            existingUser.Email = request.Email;
            existingUser.UserName = request.Email;
            existingUser.FirstName = request.FirstName;
            existingUser.LastName = request.LastName;
            existingUser.PhoneNumber = request.PhoneNumber;
            existingUser.Position = request.Position;

            await _userManager.UpdateNormalizedEmailAsync(existingUser);
            var userUpdateResult = await _userManager.UpdateAsync(existingUser);

            if (userUpdateResult.Succeeded)
            {
                return existingUser.Id;
            }
            //not sure how to handle it...
            //log userCreationResult.Errors
            //throw user createion error?
            throw new Exception("User update failed at db level.");
        }
    }
}