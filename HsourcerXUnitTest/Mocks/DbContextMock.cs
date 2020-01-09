using HSourcer.Application.Interfaces;
using HSourcer.Domain.Entities;
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
            var te = new Team() { TeamId = 1 };
            tms = new List<Team>() { te };
            var us = new User() { Id = 1, TeamId = 1, Team = te };
            users = new List<User>() { us };
        }

        public List<Absence> abs { get; set; }

        public List<Organization> org { get; set; }


        public List<Team> tms { get; set; }

        public List<User> users { get; set; }
        public Mock<IHSourcerDbContext> MockIt()
        {
            var _db = new Mock<IHSourcerDbContext>();

            var teams = MockDbSet(tms);
            //Set up mocks for db sets

            _db.Setup(m => m.Teams).Returns(teams.Object);
            var u = MockDbSet(users);
            //Set up mocks for db sets

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