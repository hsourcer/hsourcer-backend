using Microsoft.EntityFrameworkCore;
using HSourcer.Application.Interfaces;
using HSourcer.Domain.Entities;

namespace HSourcer.Persistence
{
    public class HSourcerDbContext : DbContext, IHSourcerDbContext
    {
        public HSourcerDbContext(DbContextOptions<HSourcerDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Absence> Absences { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HSourcerDbContext).Assembly);
        }
    }
}