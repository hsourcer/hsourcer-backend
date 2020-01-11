using System.Threading;
using System.Threading.Tasks;
using MediatR;
using HSourcer.Application.Interfaces;
using HSourcer.Domain.Entities;
using System;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using HSourcer.Application.Notifications.Models;
using System.Collections.Generic;
using HSourcer.Domain.Security;

namespace HSourcer.Application.Users.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IHSourcerDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly INotificationService _notificationService;

        public CreateUserCommandHandler(IHSourcerDbContext context, UserManager<User> userManager, INotificationService notificationService)
        {
            _context = context;
            _userManager = userManager;
            _notificationService = notificationService;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            //map request to User
            var newUser = new User
            {
                Active = true,
                Email = request.Email,
                UserName = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Position = request.Position,
                TeamId = request.TeamId,
                UserRole = Enum.GetName(typeof(RoleEnum), request.UserRole)
            };

            var userCreationResult = await _userManager.CreateAsync(newUser, request.Password);

            if (userCreationResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, Enum.GetName(typeof(RoleEnum), (int)(request.UserRole)));

                var passwordSubmissionToken = await _userManager.GeneratePasswordResetTokenAsync(newUser);
                return newUser.Id;
            }
            else
            {
                throw new Exception("User creation failed at user manager.");
            }
            //not sure how to handle it...
            //log userCreationResult.Errors
            //throw user creation error?
            throw new Exception("User creation failed at db level.");
        }
    }
}