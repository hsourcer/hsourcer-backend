using HSourcer.Application.Interfaces;
using HSourcer.Domain.Entities;
using Moq;
using System;
using HSourcer.Application.UserIdentity;

namespace HsourcerXUnitTest.Mocks
{
    public static class UserResolverMock
    {
        public static Mock<IUserResolve> MockIt(User u)
        {
            var _mockUserResolver = new Mock<IUserResolve>();
            _mockUserResolver.Setup(x => x.GetUserIdentity()).ReturnsAsync(u);

            return _mockUserResolver;
        }
    }
}