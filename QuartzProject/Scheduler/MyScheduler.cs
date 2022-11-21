using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;
using QuartzProject.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuartzProject.Scheduler
{
    public class MyScheduler : IHostedService
    {
        public IScheduler scheduler { get; set; } // api for interacting with scheduler
        private readonly IJobFactory _jobFactory; // producing instances of IJob
        //private readonly IEnumerable<JobMetaData> _jobMetaData;
        private readonly JobMetaData _jobMetaData;
        private readonly ISchedulerFactory _schedulerFactory; // construct a scheduler factory

        public MyScheduler(IJobFactory jobFactory, JobMetaData jobMetaData, 
            ISchedulerFactory schedulerFactory)
        {
            _jobFactory = jobFactory;
            _jobMetaData = jobMetaData;
            _schedulerFactory = schedulerFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // creating scheduler and get scheduler through factory
            scheduler = await _schedulerFactory.GetScheduler();

            // assigning the job factory
            scheduler.JobFactory = _jobFactory;


            // support for one job
            
            // create job
            IJobDetail jobDetail = CreateJob(_jobMetaData);
            // create trigger
            ITrigger trigger = CreateTrigger(_jobMetaData);

            // schedule job
            await scheduler.ScheduleJob(jobDetail, trigger, cancellationToken);
            
            //Suporrt for Multiple Jobs
            /*
            foreach (var myJob in _jobMetaData)
            {
                // create job
                IJobDetail jobDetail = CreateJob(myJob);
                // create trigger
                ITrigger trigger = CreateTrigger(myJob);

                // schedule job
                await scheduler.ScheduleJob(jobDetail, trigger, cancellationToken);
            }
            */

            // start the scheduler
            await scheduler.Start(cancellationToken);

        }

        private ITrigger CreateTrigger(JobMetaData jobMetaData)
        {
            return TriggerBuilder.Create()
                .WithIdentity(jobMetaData.JobId.ToString())
                .WithDescription(jobMetaData.JobName)
                .WithCronSchedule(jobMetaData.CronExpression)
                .Build();
        }

        private IJobDetail CreateJob(JobMetaData jobMetaData)
        {
            // creation of job
            return JobBuilder.Create(jobMetaData.JobType)
                .WithIdentity(jobMetaData.JobId.ToString())
                .WithDescription(jobMetaData.JobName)
                .Build();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await scheduler.Shutdown();
        }
    }
}
