using HSourcer.Application.Absences.Commands.Create;
using HSourcer.Application.Interfaces;
using HSourcer.Application.UserIdentity;
using HSourcer.Domain.Enums;
using Moq;
using System;
using System.Collections.Generic;
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

            var _context = new Mock<IHSourcerDbContext>();
            var _notificationService = new Mock<INotificationService>();
            var _ctoken = new CancellationToken();

            var _mockUserResolver = new Mock<IUserResolverService>();

            CreateAbsenceCommand request = new CreateAbsenceCommand();
            request.ContactPersonId = ContactPersonId;
            request.StartDate = Convert.ToDateTime(StartDate);
            request.EndDate = Convert.ToDateTime(EndDate);
            request.AbsenceType = AbsenceType;

            CreateAbsenceCommandHandler handler = new CreateAbsenceCommandHandler(_context.Object, _mockUserResolver.Object, _notificationService.Object);

            var result = await handler.Handle(request, _ctoken);

            System.Diagnostics.Debug.WriteLine("result is: ", result);

            return;
        }
    }
}
