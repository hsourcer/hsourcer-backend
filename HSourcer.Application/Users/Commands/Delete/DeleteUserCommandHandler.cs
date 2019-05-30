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
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, int>
    {
        private readonly IHSourcerDbContext _context;
        private readonly UserManager<User> _userManager;

        public DeleteUserCommandHandler(IHSourcerDbContext context, UserManager<User> _userManager) 
        {
            _context = context;
            this._userManager = _userManager;
        }

        public async Task<int> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {

            var existingUser = await _userManager.FindByIdAsync(request.UserId.ToString());

            if(existingUser == null)
                throw new NotFoundException("User", request.UserId);
            
            existingUser.Active = false;        

            var userDeleteResult = await _userManager.UpdateAsync(existingUser);

            if (userDeleteResult.Succeeded)
            {
                return existingUser.Id;
            }
            //not sure how to handle it...
            //log userCreationResult.Errors
            //throw user createion error?
            throw new Exception("User Delete failed at db level.");
        }
    }
}