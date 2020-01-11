using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace HSourcer.WebUI.BackgroundService
{
    //code from https://github.com/pgroene/ASPNETCoreScheduler/tree/master/ASPNETCoreScheduler
    public abstract class ScopedProcessor : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ScopedProcessor(IServiceScopeFactory serviceScopeFactory) : base()
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task Process()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                await ProcessInScopeAsync(scope.ServiceProvider);
            }
        }

        public abstract Task ProcessInScopeAsync(IServiceProvider serviceProvider);
    }
}
