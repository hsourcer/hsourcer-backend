using HSourcer.Domain.Entities;
using HSourcer.Domain.Security;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using HSourcer.Persistence;
using HSourcer.Application.Interfaces;

namespace HsourcerXUnitTest.Mocks
{
    public class DbContextMock
    {
        public IHSourcerDbContext CreateDb()
        {
            var options = new DbContextOptionsBuilder<HSourcerDbContext>()
                .UseInMemoryDatabase(databaseName: "testInMemory")
                .Options;
            var db = new HSourcerDbContext(options);
            try
            {
                HSourcerInitializer.Initialize(db);
            }
            catch { };
            return db;
        }
    }
}