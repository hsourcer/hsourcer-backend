using HSourcer.Application.Interfaces;
using HSourcer.Domain.Entities;
using HSourcer.Domain.Security;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HsourcerXUnitTest.Mocks
{
    public class DbContextMock
    {
        public DbContextMock()
        {
            var te1 = new Team() { TeamId = 1 };
            var te2 = new Team() { TeamId = 2 };
            tms = new List<Team>() { te1, te2 };
            var us1 = new User() {
                Id = 1,
                TeamId = 1,
                Team = te1,
                UserRole = Enum.GetName(typeof(RoleEnum), RoleEnum.TEAM_LEADER)
            };
            var us2 = new User()
            {
                Id = 2,
                TeamId = 1,
                Team = te1,
                UserRole = Enum.GetName(typeof(RoleEnum), RoleEnum.EMPLOYEE)
            };
            var us3 = new User()
            {
                Id = 2,
                TeamId = 1,
                Team = te1,
                UserRole = Enum.GetName(typeof(RoleEnum), RoleEnum.EMPLOYEE)
            };
            var us4 = new User()
            {
                Id = 3,
                TeamId = 2,
                Team = te2,
                UserRole = Enum.GetName(typeof(RoleEnum), RoleEnum.EMPLOYEE)
            };
            var us5 = new User()
            {
                Id = 5,
                TeamId = 1,
                Team = te1,
                UserRole = Enum.GetName(typeof(RoleEnum), RoleEnum.ADMIN)
            };
            users = new List<User>() { us1, us2, us3, us4, us5 };

            var org1 = new Organization() { OrganizationId = 1, Teams = tms };
            org = new List<Organization>() { org1 };
        }

        public List<Absence> abs { get; set; }

        public List<Organization> org { get; set; }

        public List<Team> tms { get; set; }

        public List<User> users { get; set; }

        public Mock<IHSourcerDbContext> MockIt()
        {
            var _db = new Mock<IHSourcerDbContext>();

            var teams = MockDbSet(tms);
            _db.Setup(m => m.Teams).Returns(teams.Object);

            var o = MockDbSet(org);
            _db.Setup(m => m.Organizations).Returns(o.Object);

            var u = MockDbSet(users);
            _db.Setup(m => m.Users).Returns(u.Object);
            return _db;
        }
        public static Mock<DbSet<T>> MockDbSet<T>(IEnumerable<T> list) where T : class, new()
        {
            IQueryable<T> queryableList = list.AsQueryable();
            Mock<DbSet<T>> dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.As<IQueryable<T>>().Setup(x => x.Provider).Returns(queryableList.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.Expression).Returns(queryableList.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.ElementType).Returns(queryableList.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.GetEnumerator()).Returns(() => queryableList.GetEnumerator());

            dbSetMock.As<IQueryable<T>>().Setup(x => x.Provider).Returns(queryableList.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.Expression).Returns(queryableList.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.ElementType).Returns(queryableList.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.GetEnumerator()).Returns(() => queryableList.GetEnumerator());



            return dbSetMock;
        }
    }
}