using Microsoft.EntityFrameworkCore;
using HSourcer.Persistence.Infrastructure;

namespace HSourcer.Persistence
{
    public class HSourcerDbContextFactory : DesignTimeDbContextFactoryBase<HSourcerDbContext>
    {
        protected override HSourcerDbContext CreateNewInstance(DbContextOptions<HSourcerDbContext> options)
        {
            return new HSourcerDbContext(options);
        }
    }
}

