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
using System.Collections.Generic;

using Microsoft.AspNetCore.Http;
using HSourcer.Application.UserIdentity;
using HsourcerXUnitTest.Mocks;

namespace HsourcerXUnitTest
{
    public class UpdateUserCommandTests
    {

        Mock<IHSourcerDbContext> _context = new Mock<IHSourcerDbContext>();
        CancellationToken ctoken = new CancellationToken();

        [Theory(DisplayName = "Update user")]
        [InlineData(1, "string1", "string2", "developer", "123", "string2@asdf.pl", RoleEnum.EMPLOYEE)]
        public async Task HandleUpdateUser(int UserId, string FirstName, string LastName, string Position, string PhoneNumber, string Email, RoleEnum UserRole)
        {
            var _mockUserResolver = new Mock<IUserResolve>();
            var dbMocker = new DbContextMock();
            // Get Admin user as current user
            _mockUserResolver.Setup(x => x.GetUserIdentity()).ReturnsAsync(
              dbMocker.users[dbMocker.users.Count - 1]
            );

            List<User> ls = new List<User>()
            {
                new User(){
                Id = 1,
                FirstName =""
                }
            };

            var _userManager = MockUserManager(ls);

            UpdateUserCommand request = new UpdateUserCommand();
            request.FirstName = FirstName;
            request.LastName = LastName;
            request.Position = Position;
            request.PhoneNumber = PhoneNumber;
            request.Email = Email;
            request.UserId = UserId;
            // request.UserRole = UserRole;

            UpdateUserCommandHandler handler = new UpdateUserCommandHandler(_context.Object, _userManager.Object, _mockUserResolver.Object);

            // var result = await handler.Handle(request, ctoken);

            //System.Diagnostics.Debug.WriteLine("result is: ", result);

            // Returned integer is exit code, 0 for Success
            // Assert.IsType<int>(result);
            // Assert.True(result == 0);
        }

        public static Mock<UserManager<TUser>> MockUserManager<TUser>(List<TUser> ls) where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var dbContext = new DbContextMock();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<TUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

            mgr.Setup(x => x.DeleteAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<TUser, string>((x, y) => ls.Add(x));
            mgr.Setup(x => x.UpdateAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
            // mgr.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(dbContext.users.ToArray()[0]);

            return mgr;
        }
    }
}

