using HSourcer.Application.Absences.Commands;
using HSourcer.Application.Interfaces;
using HsourcerXUnitTest.Mocks;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HsourcerXUnitTest.Application.UnitTests.Absences.Commands.DeleteAbsence
{
    public class DeleteAbsenceCommandTest
    {
        CancellationToken _ctoken = new CancellationToken();
        [Theory(DisplayName = "Delete absence")]
        [InlineData(1)]
        public async Task<int> HandleDeleteAbsence(int AbsenceId)
        {

            var _dbC = new DbContextMock();
            var _db = _dbC.CreateDb();

            var _mockUserResolver = UserResolverMock.MockIt(_db.Users.First());


            DeleteAbsenceCommand request = new DeleteAbsenceCommand();
            request.AbsenceId = AbsenceId;

            DeleteAbsenceCommandHandler handler = new DeleteAbsenceCommandHandler(_db, _mockUserResolver.Object);

            var result = await handler.Handle(request, _ctoken);
            Assert.IsType<int>(result);
            Assert.True(result != 0);

            return 0;
        }

        [Theory(DisplayName = "Failing absence deletion")]
        [InlineData(9999)]
        public async Task<int> HandleFailingDeleteAbsence(int AbsenceId)
        {

            var _notificationService = new Mock<INotificationService>();
            _notificationService.Setup(x => x.SendAsync(new HSourcer.Application.Notifications.Models.Message()));
            var _dbC = new DbContextMock();
            var _db = _dbC.CreateDb();

            var _mockUserResolver = UserResolverMock.MockIt(_db.Users.First());


            DeleteAbsenceCommand request = new DeleteAbsenceCommand();
            request.AbsenceId = AbsenceId;

            DeleteAbsenceCommandHandler handler = new DeleteAbsenceCommandHandler(_db, _mockUserResolver.Object);

            try
            {
                var result = await handler.Handle(request, _ctoken);
            }
            catch (Exception e)
            {
                Assert.Equal("Absence does not exists, or you have no permission to delete it.", e.Message);
            }
            return 0;
        }

    }
}
