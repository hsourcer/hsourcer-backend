using Microsoft.EntityFrameworkCore;
using HSourcer.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace HSourcer.Application.Interfaces
{
    public interface IHSourcerDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Team> Teams { get; set; }
        DbSet<Absence> Absences { get; set; }
        DbSet<Organization> Organizations { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

