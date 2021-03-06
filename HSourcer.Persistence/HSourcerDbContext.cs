using Microsoft.EntityFrameworkCore;
using HSourcer.Application.Interfaces;
using HSourcer.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;

namespace HSourcer.Persistence
{
    public class HSourcerDbContext : IdentityDbContext<User, IdentityRole<int>, int>, IHSourcerDbContext
    {
        public HSourcerDbContext(DbContextOptions<HSourcerDbContext> options)
            : base(options)
        {
        }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Absence> Absences { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HSourcerDbContext).Assembly);

        }
    }
}