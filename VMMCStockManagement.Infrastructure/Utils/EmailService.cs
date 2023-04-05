using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Models;
using VMMCStockManagement.Domain.Utils;

namespace VMMCStockManagement.Infrastructure.Utils
{
	public class EmailService : IEmailService
	{
		private readonly IConfiguration configuration;
		private readonly ILogger<EmailService> logger;
		public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
		{
			this.configuration = configuration;
			this.logger = logger;
		}

		public void Send(Email email)
		{
			var smtpServer = configuration.GetValue<string>("MailSettings:Host");
			var port = configuration.GetValue<int>("MailSettings:Port");
			var username = configuration.GetValue<string>("MailSettings:Username");
			var password = configuration.GetValue<string>("MailSettings:Password");

			Task.Factory.StartNew(() =>
			{

				using (var client = new MailKit.Net.Smtp.SmtpClient())
				{
					try
					{
						var message = CreateEmail(email);

						client.Connect(smtpServer, port,
							SecureSocketOptions.StartTls);

						client.AuthenticationMechanisms.Remove("XOAUTH2");
						client.Authenticate(username, password);
						client.Send(message);
					}
					catch (Exception ex)
					{
						logger.LogError(ex.Message);
					}
					finally
					{
						client.Disconnect(true);
						client.Dispose();
					}
				}

			});
		}

		private MimeMessage CreateEmail(Email email)
		{
			var msg = new MimeMessage();
			var emailFrom = configuration.GetValue<string>("MailSettings:From");
			var name = configuration.GetValue<string>("MailSettings:DisplayName");
			msg.From.Add(MailboxAddress.Parse(emailFrom));
			msg.To.AddRange(email.To);

			if (email.CC != null && email.CC.Count() > 0)
				msg.Cc.AddRange(email.CC);

			msg.Subject = email.Subject;
			var bodyBuilder = new BodyBuilder();
			bodyBuilder.HtmlBody = email.Message;
			msg.Body = bodyBuilder.ToMessageBody();

			return msg;
		}
	}
}
