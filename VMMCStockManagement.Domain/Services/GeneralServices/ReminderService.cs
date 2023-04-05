using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Interfaces.Repositories;
using VMMCStockManagement.Domain.Interfaces.Services;
using VMMCStockManagement.Domain.Models;
using VMMCStockManagement.Domain.Models.Requests;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Utils;

namespace VMMCStockManagement.Domain.Services.GeneralServices
{
	public class ReminderService : IReminderService
	{

		private readonly IQueryRepository<StockRequest, StockRequestFilter> stockQueryRepository;
		private readonly UserManager<User> userManager;
		private readonly IEmailService emailService;

		public ReminderService(IQueryRepository<StockRequest, StockRequestFilter> stockQueryRepository,
			UserManager<User> userManager, IEmailService emailService)
		{
			this.stockQueryRepository = stockQueryRepository;
			this.userManager = userManager;
			this.emailService = emailService;

		}

		public async Task ProcessReminder(List<string> roles, int numOfDays = 7)
		{
			var now = DateTime.Now.AddDays(-numOfDays);
			
			var requests = stockQueryRepository
				.GetAll()
				.Where(x => x.RequestApproval.ItStaffStatus != Enums.StockStatus.Completed && ((x.CreatedAt.Date - now.Date).TotalDays % 3 == 0))
				.ToList();

			if (requests.Any())
			{
				await SendReminder(roles, requests);
			}

		}

		private async Task SendReminder(List<string> roles, List<StockRequest> requests)
		{

			var usersToSend = new List<User>();

			foreach (var role in roles)
			{
				var users = await userManager.GetUsersInRoleAsync(role);

				usersToSend.AddRange(users);
			}

			if (usersToSend.Any())
			{
				//Send emails
				Dictionary<string, string> emails = new Dictionary<string, string>();

				foreach (var user in usersToSend)
				{

					emails.Add(user.Email, user.Email);
				}


				//Building individual email per request
				foreach (var request in requests)
				{

					var message = $"Hi Team, <br> <br> This email is sent as a reminder to you.<br> <br> There is a request by this reference number {request.ReferenceNumber} that need assets to be procured.";
					var email = Email.Create(emails, null, "IT Assets - Procurement Reminder", message);
					emailService.Send(email);
				}
			}
		}

	}
}
