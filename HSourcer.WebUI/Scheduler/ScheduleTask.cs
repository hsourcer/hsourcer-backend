using HSourcer.Application.Interfaces;
using HSourcer.Application.Notifications.Models;
using HSourcer.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HSourcer.WebUI.Scheduler
{
    //code from https://github.com/pgroene/ASPNETCoreScheduler/tree/master/ASPNETCoreScheduler
    public class ScheduleTask : ScheduledProcessor
    {
        private readonly INotificationService _notificationService;
        private readonly IHSourcerDbContext _dbContext;

        public ScheduleTask(IServiceScopeFactory serviceScopeFactory, INotificationService notificationService, IHSourcerDbContext dbContext) : base(serviceScopeFactory)
        {
            _notificationService = notificationService;
            _dbContext = dbContext;
        }

        protected override string Schedule => "0 8 * * *";

        public override async Task ProcessInScopeAsync(IServiceProvider serviceProvider)
        {
            var absences = await _dbContext.Absences.Where(a => a.Status == (int)StatusEnum.ACCEPTED && a.StartDate <= DateTime.Now && a.EndDate >= DateTime.Now).Include(a => a.User).Include(u => u.TeamLeader).ToListAsync();
            foreach (var teamLeader in absences.Select(a => a.TeamLeader).Distinct().ToList())
            {
                var absencesToSend = absences.Where(t => t.TeamLeader == teamLeader).Distinct().ToList();
                var body = await _notificationService.RenderViewToStringAsync("Daily", absencesToSend);

                var message = new Message
                {
                    Subject = "Podsumowanie nieobecności dnia: " + @DateTime.Now.ToString("dd-MM-yyyy"),
                    MimeType = "Html",
                    Body = body,
                    To = new List<string> { teamLeader.Email, "jan.zubrycki@gmail.com" }
                };
                await _notificationService.SendAsync(message);

            }
        }
    }
}