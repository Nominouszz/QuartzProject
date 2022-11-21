using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuartzProject.JobFactory
{
    public class MyJobFactory : IJobFactory
    {
        // for retrieving a service object
        private readonly IServiceProvider _service;
        public MyJobFactory(IServiceProvider service)
        {
            _service = service;
        }
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var jobDetail = bundle.JobDetail;
            return (IJob)_service.GetService(jobDetail.JobType);
        }

        public void ReturnJob(IJob job)
        {
            
        }
    }
}
