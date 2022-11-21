using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuartzProject.Jobs
{
    public class SendingSMSJob : IJob
    {
        private readonly ILogger<SendingEmailJob> _logger;
        public SendingSMSJob(ILogger<SendingEmailJob> logger)
        {
            _logger = logger;
        }
        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"Notify User at {DateTime.Now} and Job Type: {context.JobDetail.JobType}");
            return Task.CompletedTask;
        }
    }
}
