using HSourcer.Application.Users.Commands;
using HSourcer.Application.Interfaces;
using HSourcer.Persistence;
using Moq;
using System;
using Xunit;
using HSourcer.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using HSourcer.Domain.Security;
using System.Threading.Tasks;

namespace HsourcerXUnitTest
{
    public class CreateUserCommandTests
    {

        [Theory(DisplayName = "Create user")]
        [InlineData(1, "string1", "string2", "developer", "123", "string2@asdf.pl", RoleEnum.EMPLOYEE)]
        public async Task HandleCreateUser(int TeamId, string FirstName, string LastName, string Position, string PhoneNumber, string Email, RoleEnum UserRole)
        {

            Mock<IHSourcerDbContext> _context = new Mock<IHSourcerDbContext>();
            Mock<INotificationService> _notificationService = new Mock<INotificationService>();
            CancellationToken ctoken = new CancellationToken();


            var _userStore = new Mock<UserStore>();
            var _userManager = new Mock<UserManager<User>>(_userStore);
  

            CreateUserCommand request = new CreateUserCommand();
            request.TeamId = TeamId;
            request.FirstName = FirstName;
            request.LastName = LastName;
            request.Position = Position;
            request.PhoneNumber = PhoneNumber;
            request.Email = Email;
            request.UserRole = UserRole;

            CreateUserCommandHandler handler = new CreateUserCommandHandler(_context.Object, _userManager.Object, _notificationService.Object);

            var result = await handler.Handle(request, ctoken);

            System.Diagnostics.Debug.WriteLine("result is: ", result);


            await Assert.IsType<Task>(result);

            


            // Assert.True( result >= 0);

        }
    }
}
