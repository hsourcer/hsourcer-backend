using HSourcer.Application.Absences.Commands.Create;
using HSourcer.Domain.Entities;
using HsourcerXUnitTest.Mocks;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HsourcerXUnitTest.Application.UnitTests.Teams
{
    public class CreateTeamCommandTest
    {

        CancellationToken _ctoken = new CancellationToken();
        [Theory(DisplayName = "Create team")]
        [InlineData("Team1", "Some team", 1, 2)]
        [InlineData("Team1", "Some team", 2, 1)]
        public async Task<int> HandleCreateTeam(string Name, string Description, int UserId1, int UserId2)
        {

            var _dbC = new DbContextMock();
            var _db = _dbC.CreateDb();

            var _mockUserResolver = UserResolverMock.MockIt(_db.Users.First());

            CreateTeamCommand request = new CreateTeamCommand();
            request.Name = Name;
            request.Description = Description;
            request.Users = new List<int> { UserId1, UserId2 };

            CreateTeamCommandHandler handler = new CreateTeamCommandHandler(_db, _mockUserResolver.Object);

            var result = await handler.Handle(request, _ctoken);
            Assert.IsType<int>(result);
            Assert.True(result != 0);

            return 0;
        }

        [Theory(DisplayName = "Failing team creation")]
        [InlineData("Team1", "Some team", 9999)]
        [InlineData("Team1", "Some team", -1)]
        public async Task<int> HandleFailingCreateTeam(string Name, string Description, int UserId)
        {

            var _dbC = new DbContextMock();
            var _db = _dbC.CreateDb();

            var _mockUserResolver = UserResolverMock.MockIt(_db.Users.First());


            CreateTeamCommand request = new CreateTeamCommand();
            request.Name = Name;
            request.Description = Description;
            request.Users = new List<int> { UserId };

            CreateTeamCommandHandler handler = new CreateTeamCommandHandler(_db, _mockUserResolver.Object);

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
