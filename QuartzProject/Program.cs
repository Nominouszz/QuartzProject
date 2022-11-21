using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using QuartzProject.JobFactory;
using QuartzProject.Jobs;
using QuartzProject.Models;
using QuartzProject.Scheduler;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace QuartzProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {

                    services.AddSingleton<IJobFactory, MyJobFactory>();
                    services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

                    #region Adding JobType
                    services.AddSingleton<NotificationJob>();
                    //services.AddSingleton<SendingEmailJob>();
                    //services.AddSingleton<LoggerJob>();
                    #endregion

                    // services.AddScoped<JobMetaData>();

                    #region Adding Jobs 
                    services.AddSingleton(new JobMetaData(Guid.NewGuid(), typeof(NotificationJob), "Notify Job", "0/5 * * * * ? *")); //5 sec working

                    //services.AddSingleton(new JobMetaData(Guid.NewGuid(), typeof(SendingSMSJob), "Sending SMS", "* * * ? * *"));
                    //services.AddSingleton(new JobMetaData(Guid.NewGuid(), typeof(SendingEmailJob), "Sending Email", ConfigurationManager.AppSettings["CronExp-5min"]));
                    #endregion



                    services.AddHostedService<MyScheduler>();
                });
    }
}
