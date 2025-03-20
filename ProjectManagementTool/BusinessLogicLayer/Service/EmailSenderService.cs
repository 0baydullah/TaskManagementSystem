using BusinessLogicLayer.IService;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Service
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly IConfiguration _configuration;

        public EmailSenderService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string ToEmail, string Subject, string Body, bool IsBodyHtml = false)
        {
            try
            {
                string? MailServer = _configuration["EmailSettings:MailServer"];
                string? FromEmail = _configuration["EmailSettings:FromEmail"];
                string? Password = _configuration["EmailSettings:Password"];
                string? SenderName = _configuration["EmailSettings:SenderName"];
                int Port = Convert.ToInt32(_configuration["EmailSettings:MailPort"]);

                var client = new SmtpClient(MailServer, Port)
                {
                    Credentials = new NetworkCredential(FromEmail, Password),
                    EnableSsl = true,
                };

                MailAddress fromAddress = new MailAddress(FromEmail, SenderName);

                MailMessage mailMessage = new MailMessage
                {
                    From = fromAddress,
                    Subject = Subject,
                    Body = Body,
                    IsBodyHtml = IsBodyHtml
                };

                mailMessage.To.Add(ToEmail);
                await client.SendMailAsync(mailMessage);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
