using System;
using AutoMapper;
using HSourcer.Persistence;
using Xunit;

namespace HSourcer.Application.Tests.Infrastructure
{
    public class QueryTestFixture : IDisposable
    {
        public HSourcerDbContext Context { get; private set; }
        public IMapper Mapper { get; private set; }

        public QueryTestFixture()
        {
            Context = HSourcerContextFactory.Create();
            Mapper = AutoMapperFactory.Create();
        }

        public void Dispose()
        {
            HSourcerContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}
