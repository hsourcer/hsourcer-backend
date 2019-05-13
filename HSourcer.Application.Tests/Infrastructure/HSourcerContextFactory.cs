using System;
using Microsoft.EntityFrameworkCore;
using HSourcer.Domain.Entities;
using HSourcer.Persistence;

namespace HSourcer.Application.Tests.Infrastructure
{
    public class HSourcerContextFactory
    {
        public static HSourcerDbContext Create()
        {
            var options = new DbContextOptionsBuilder<HSourcerDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new HSourcerDbContext(options);

            context.Database.EnsureCreated();

            context.SaveChanges();

            return context;
        }

        public static void Destroy(HSourcerDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
