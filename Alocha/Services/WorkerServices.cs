using Alocha.Domain;
using Alocha.Domain.Entities;
using Alocha.Domain.Interfaces;
using Alocha.WebUi.Helpers;
using Alocha.WebUi.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Alocha.WebUi.Services
{
    public class WorkerServices : IHostedService, IDisposable
    {
        private Timer _timer;
        private IServiceProvider _services { get; }
        private ILogger<WorkerServices> _logger;

        public WorkerServices(IServiceProvider services, ILogger<WorkerServices> logger)
        {
            _services = services;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(4));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            var tomorrowDate = DateTime.Today.AddDays(1);
            var list = new List<string>();
            using (var scope = _services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var sows = context.Sow.Where(s => !s.IsRemoved && s.DateBorn == tomorrowDate && !s.IsSmsSend
                        || !s.IsRemoved && s.DateDetachment == tomorrowDate && !s.IsSmsSend
                        || !s.IsRemoved && s.DateInsimination == tomorrowDate && !s.IsSmsSend
                        || !s.IsRemoved && s.VaccineDate == tomorrowDate && !s.IsSmsSend).ToList();
                foreach (var item in sows)
                {
                    if (item.User.PhoneNumberConfirmed)//If number is confirm 
                    {
                        if (!list.Contains(item.UserId))
                        {
                            list.Add(item.UserId);
                            var userSows = sows.Where(u => u.UserId == item.UserId);
                            var message = "Aplikacja Alocha.\nJutrzejsze wydarzenia:";
                            foreach (var sow in userSows)
                            {
                                string status = "";
                                switch (sow.Status)
                                {
                                    case "Prośna":
                                        status = "Poród";
                                        if (sow.VaccineDate == tomorrowDate)
                                            status = "Szczepienie";
                                        break;
                                    case "Luźna":
                                        status = "Inseminacja";
                                        break;
                                    case "Laktacja":
                                        status = "Oderwanie prosiąt";
                                        break;
                                }
                                message += string.Format("\nLocha nr {0} - {1}", sow.Number, status);
                                sow.IsSmsSend = true;
                                context.SaveChanges();
                            }
                            SmsSender.SendSmsAsync(string.Format("48{0}", item.User.PhoneNumber), message).GetAwaiter().GetResult();
                            _logger.LogInformation("Wysłano sms pod numer ");
                               
                        }
                    }
                };
            }            
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

    }
}
