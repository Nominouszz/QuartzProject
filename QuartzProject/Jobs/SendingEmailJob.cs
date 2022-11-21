using FluentEmail.Core;
using FluentEmail.Smtp;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace QuartzProject.Jobs
{
    public class SendingEmailJob : IJob
    {
        private readonly ILogger<SendingEmailJob> _logger;
        public SendingEmailJob(ILogger<SendingEmailJob> logger)
        {
            _logger = logger;
        }
        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"Notify User at {DateTime.Now} and Job Type: {context.JobDetail.JobType}");
            string fromMail = "darrylvillanueva12@gmail.com";
            string fromPassword = "ohqjbophvokmpbjq";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Test Subject";
            message.To.Add(new MailAddress("dominicmanigos12@gmail.com"));
            message.Body = "<html><body>Hello Test!</body></html>";
            message.IsBodyHtml = true;

            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(fromMail, fromPassword);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            return Task.CompletedTask;
        }
    }
}
