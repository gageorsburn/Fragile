using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Fragile.Models;

namespace Fragile.Services
{
    public class EmailService
    {
        public SendGrid.Web SendGridClient { get; private set; }

        public IUrlHelper Url { get; }
        public ApplicationDbContext DbContext { get; }
        public HttpContext HttpContext { get; }

        public EmailService(ApplicationDbContext dbContext, IUrlHelper urlHelper, IHttpContextAccessor contextAccessor, IConfiguration configuration)
        {
            DbContext = dbContext;
            Url = urlHelper;
            HttpContext = contextAccessor.HttpContext;
            SendGridClient = new SendGrid.Web(new System.Net.NetworkCredential(configuration["Credentials:SendGrid:Id"], configuration["Credentials:SendGrid:Secret"]));
        }

        public async void SendResetPasswordToken(string Email, string ResetPasswordToken)
        {
            string resetLink = Url.Action("ChangePassword", "Member", new { Email = Email, ResetPasswordToken = ResetPasswordToken }, HttpContext.Request.IsHttps ? "https" : "http", HttpContext.Request.Host.ToUriComponent());

            var passwordResetMessage = new SendGrid.SendGridMessage
            {
                From = new System.Net.Mail.MailAddress("no-reply@fragilesoftware.com", "Fragile Software"),
                Subject = "Reset your password",
                Text = "Copy and paste the following link to reset your password.\r\n" + resetLink,
                Html = "<a href='" + resetLink + "'>Click here to reset your password.</a>"
            };
            passwordResetMessage.AddTo(Email);

            await SendGridClient.DeliverAsync(passwordResetMessage);
        }

        public async void SendContactNotification(string Email, string Message)
        {
            var notificationMessage = new SendGrid.SendGridMessage
            {
                From = new System.Net.Mail.MailAddress("no-reply@fragilesoftware.com", "Fragile Software"),
                Subject = "Contact Notification from " + Email,
                Text = Message
            };

            var emails = DbContext.TeamMember.Select(m => m.Email).ToList();

            foreach (var email in emails)
                notificationMessage.AddTo(email);

            await SendGridClient.DeliverAsync(notificationMessage);
        }
    }
}
