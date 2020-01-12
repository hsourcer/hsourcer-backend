using HSourcer.Application.Absences.Commands.Update;
using HSourcer.Application.Interfaces;
using HSourcer.Domain.Enums;
using HsourcerXUnitTest.Mocks;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HsourcerXUnitTest.Application.UnitTests.Absences.Commands.UpdateAbsence
{
    public class UpdateAbsenceCommandTest
    {
        CancellationToken _ctoken = new CancellationToken();
        [Theory(DisplayName = "Update absence")]
        [InlineData(1, "comment!", StatusEnum.PENDING)]
        public async Task<int> HandleCreateAbsence(int AbsenceId, string Comment, StatusEnum Status)
        {

            var _notificationService = new Mock<INotificationService>();
            _notificationService.Setup(x => x.SendAsync(new HSourcer.Application.Notifications.Models.Message()));
            var _dbC = new DbContextMock();
            var _db = _dbC.CreateDb();

            var _mockUserResolver = UserResolverMock.MockIt(_db.Users.First());


            UpdateAbsenceCommand request = new UpdateAbsenceCommand();
            request.Status = Status;
            request.Comment = Comment;
            request.AbsenceId = AbsenceId;


            UpdateAbsenceCommandHandler handler = new UpdateAbsenceCommandHandler(_db, _mockUserResolver.Object, _notificationService.Object);

            var result = await handler.Handle(request, _ctoken);
            Assert.IsType<int>(result);
            Assert.True(result != 0);

            return 0;
        }

        [Theory(DisplayName = "Failing absence update")]
        [InlineData(9999, "comment!", StatusEnum.PENDING)]
        public async Task<int> HandleFailingCreateAbsence(int AbsenceId, string Comment, StatusEnum Status)
        {

            var _notificationService = new Mock<INotificationService>();
            _notificationService.Setup(x => x.SendAsync(new HSourcer.Application.Notifications.Models.Message()));
            var _dbC = new DbContextMock();
            var _db = _dbC.CreateDb();

            var _mockUserResolver = UserResolverMock.MockIt(_db.Users.First());


            UpdateAbsenceCommand request = new UpdateAbsenceCommand();
            request.Status = Status;
            request.Comment = Comment;
            request.AbsenceId = AbsenceId;


            UpdateAbsenceCommandHandler handler = new UpdateAbsenceCommandHandler(_db, _mockUserResolver.Object, _notificationService.Object);

            try
            {
                var result = await handler.Handle(request, _ctoken);
            }
            catch (NullReferenceException) { }
            catch
            {
                Assert.Equal('a', 'b');
            }

            return 0;
        }
    }
}
