using HSourcer.Application.Absences.Commands.Create;
using HSourcer.Application.Interfaces;
using HSourcer.Domain.Enums;
using HsourcerXUnitTest.Mocks;
using Moq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HsourcerXUnitTest.Application.UnitTests.Absences.Commands.CreateAbsence
{
    public class CreateAbsenceCommandTests
    {
        CancellationToken _ctoken = new CancellationToken();
        [Theory(DisplayName = "Create absence")]
        [InlineData(1, "2019-01-01", "2019-01-04", AbsenceEnum.SICK_LEAVE)]
        public async Task<int> HandleCreateAbsence(int ContactPersonId, string StartDate, string EndDate, AbsenceEnum AbsenceType)
        {
           
            var _notificationService = new Mock<INotificationService>();
            _notificationService.Setup(x => x.SendAsync(new HSourcer.Application.Notifications.Models.Message()));
            var _dbC = new DbContextMock();
            var _db = _dbC.CreateDb();

            var _mockUserResolver = UserResolverMock.MockIt(_db.Users.First());


            CreateAbsenceCommand request = new CreateAbsenceCommand();
            request.ContactPersonId = ContactPersonId;
            request.StartDate = Convert.ToDateTime(StartDate);
            request.EndDate = Convert.ToDateTime(EndDate);
            request.AbsenceType = AbsenceType;

            // _db.Setup(w => w.Absences.FirstOrDefaultAsync(It.IsAny<bool>())).ReturnsAsync(_db.Object.Absences[0]);

            CreateAbsenceCommandHandler handler = new CreateAbsenceCommandHandler(_db, _mockUserResolver.Object, _notificationService.Object);

            var result = await handler.Handle(request, _ctoken);
            Assert.IsType<int>(result);
            Assert.True(result != 0);

            return 0;
        }

        [Theory(DisplayName = "Fail create absence")]
        [InlineData(9999, "2019-01-01", "2019-01-04", AbsenceEnum.SICK_LEAVE)]
        [InlineData(-1, "2019-01-01", "2019-01-04", AbsenceEnum.SICK_LEAVE)]
        public async Task<int> HandleCreateAbsenceFail(int ContactPersonId, string StartDate, string EndDate, AbsenceEnum AbsenceType)
        {

            var _notificationService = new Mock<INotificationService>();
            _notificationService.Setup(x => x.SendAsync(new HSourcer.Application.Notifications.Models.Message()));
            var _dbC = new DbContextMock();
            var _db = _dbC.CreateDb();

            var _mockUserResolver = UserResolverMock.MockIt(_db.Users.First());


            CreateAbsenceCommand request = new CreateAbsenceCommand();
            request.ContactPersonId = ContactPersonId;
            request.StartDate = Convert.ToDateTime(StartDate);
            request.EndDate = Convert.ToDateTime(EndDate);
            request.AbsenceType = AbsenceType;

            // _db.Setup(w => w.Absences.FirstOrDefaultAsync(It.IsAny<bool>())).ReturnsAsync(_db.Object.Absences[0]);

            CreateAbsenceCommandHandler handler = new CreateAbsenceCommandHandler(_db, _mockUserResolver.Object, _notificationService.Object);

            try
            {
                var result = await handler.Handle(request, _ctoken);
            } 
            catch (NullReferenceException) {} 
            catch
            {
                Assert.Equal('a', 'b');
            }

            return 0;
        }
    }
}
