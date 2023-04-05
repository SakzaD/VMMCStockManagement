using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Interfaces.Repositories;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Responses;
using VMMCStockManagement.Domain.Services;

namespace VMMCStockManagement.Infrastructure.Repositories
{
	public class UserQueryRepository : IUserQueryRepository
	{
		private readonly RoleManager<Role> roleManager;
		private readonly ILogger<AuthenticateService> logger;
		private readonly UserManager<User> userManager;

		public UserQueryRepository(UserManager<User> userManager,
			RoleManager<Role> roleManager, ILogger<AuthenticateService> logger)
		{
			this.logger = logger;
			this.userManager = userManager;
			this.roleManager = roleManager;
		}


		public IQueryable<User> Filter(UserFilter filter)
		{

			var users = userManager.Users;

			if (filter != null && !string.IsNullOrEmpty(filter.UserId))
			{
				users = users.Where(x => x.Id == filter.UserId);
			}

			return users;
		}

		public async Task<IList<User>> GetUsersByRole(string role)
		{

			return await userManager.GetUsersInRoleAsync(role);
		}

		public User? GetUserById(string userId)
		{
			return userManager.Users.FirstOrDefault(x => x.Id == userId);
		}

		public User? GetUserByEmployeeNumber(string empNumber)
		{
			return userManager.Users.FirstOrDefault(x => x.EmployeeNumber == empNumber);
		}

		public async Task<IList<User>> GetUsersByRoles(List<string> roles)
		{
			List<User> users = new List<User>();
			foreach (var role in roles)
			{
				var usersByRole = await userManager.GetUsersInRoleAsync(role);
				if (usersByRole != null)
					users.AddRange(usersByRole);
			}

			return users;
		}

		public IList<User> GetUsersByIds(List<string> ids)
		{
			var users = userManager.Users.Where(x => ids.Contains(x.Id)).ToList();

			return users;
		}

		public User? GetUserByIdNumber(string idNumber)
		{
			return userManager.Users.FirstOrDefault(x => x.IdNumber == idNumber);
		}

		public User? GetUserByUsername(string email)
		{
			return userManager.Users.FirstOrDefault(x => x.UserName == email);
		}

		public ObjectResponse<UserResponse> SearchByEmployeeNumber(string? searchValue)
		{
			var response = new ObjectResponse<UserResponse>();
			try
			{

				var user = userManager.Users.FirstOrDefault(x => x.EmployeeNumber == searchValue);

				var userRes = new UserResponse
				{
					Id = user.Id,
					Username = user.UserName,
					FirstName = user.FirstName,
					LastName = user.LastName,
					Email = user.Email,
					PhoneNumber = user.PhoneNumber
				};

				response.Data = userRes;
				response.CodeStatus = Domain.Enums.ResponseStatus.Success;


			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message);
			}
			return response;
		}
	}
}
