using System;
using HSourcer.Persistence;

namespace HSourcer.Application.Tests.Infrastructure
{
    public class CommandTestBase : IDisposable
    {
        protected readonly HSourcerDbContext _context;

        public CommandTestBase()
        {
            _context = HSourcerContextFactory.Create();
        }

        public void Dispose()
        {
            HSourcerContextFactory.Destroy(_context);
        }
    }
}
