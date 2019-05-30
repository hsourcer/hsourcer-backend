using System.Threading;
using System.Threading.Tasks;
using MediatR;
using HSourcer.Application.Interfaces;
using HSourcer.Domain.Entities;
using System;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using HSourcer.Application.Exceptions;

namespace HSourcer.Application.Users.Commands
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, int>
    {
        private readonly IHSourcerDbContext _context;
        private readonly UserManager<User> _userManager;

        public UpdateUserCommandHandler(IHSourcerDbContext context, UserManager<User> _userManager) 
        {
            _context = context;
            this._userManager = _userManager;
        }

        public async Task<int> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {

            var existingUser = await _userManager.FindByIdAsync(request.UserId.ToString());

            if(existingUser == null)
                throw new NotFoundException("User", request.UserId);
            
            existingUser.Active = request.Active;
            existingUser.Email = request.Email;
            existingUser.UserName = request.Email;
            existingUser.FirstName = request.FirstName;
            existingUser.LastName = request.LastName;
            existingUser.PhoneNumber = request.PhoneNumber;
            existingUser.Position = request.Position;
        

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