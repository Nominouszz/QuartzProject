using System;
using System.Collections.Generic;
using System.Text;

namespace QuartzProject.Models
{
    public class JobMetaData
    {

        public Guid JobId { get; set; }
        public Type JobType { get; }
        public string JobName { get; }
        public string CronExpression { get; }
        public JobMetaData(Guid jobId, Type jobType, string jobName, string cronExpression)
        {
            JobId = jobId;
            JobType = jobType;
            JobName = jobName;
            CronExpression = cronExpression;
        }
        
    }
}
