using HSourcer.Application.Absences.Commands.Create;
using HSourcer.Application.Interfaces;
using HSourcer.Application.UserIdentity;
using HSourcer.Domain.Entities;
using HSourcer.Domain.Enums;
using HsourcerXUnitTest.Mocks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HsourcerXUnitTest.Application.UnitTests.Absences.Commands.CreateAbsence
{
    public class CreateAbsenceCommandTests
    {
        [Theory(DisplayName = "Create absence")]
        [InlineData(1, "2019-01-01", "2019-01-04", AbsenceEnum.SICK_LEAVE)]
        public async Task HandleCreateAbsence(int ContactPersonId, string StartDate, string EndDate, AbsenceEnum AbsenceType)
        {

            /*var _notificationService = new Mock<INotificationService>();

            _notificationService.Setup(x => x.SendAsync(new HSourcer.Application.Notifications.Models.Message()));
            var dbMocker = new DbContextMock();
            var _db = dbMocker.MockIt();

            var _mockUserResolver = new Mock<IUserResolve>();
          
            _mockUserResolver.Setup(x => x.GetUserIdentity()).ReturnsAsync(

              dbMocker.users.First()
          );
            var _ctoken = new CancellationToken();
           
           


            
            CreateAbsenceCommand request = new CreateAbsenceCommand();
            request.ContactPersonId = ContactPersonId;
            request.StartDate = Convert.ToDateTime(StartDate);
            request.EndDate = Convert.ToDateTime(EndDate);
            request.AbsenceType = AbsenceType;

            CreateAbsenceCommandHandler handler = new CreateAbsenceCommandHandler(_db.Object, _mockUserResolver.Object, _notificationService.Object);

            var result = await handler.Handle(request, _ctoken);

            System.Diagnostics.Debug.WriteLine("result is: ", result);
            */
            return;
        }
        public static Mock<UserManager<TUser>> MockUserManager<TUser>(List<TUser> ls) where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<TUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

            mgr.Setup(x => x.DeleteAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<TUser, string>((x, y) => ls.Add(x));
            mgr.Setup(x => x.UpdateAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);

            return mgr;
        }
    }
}
