using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using VMMCStockManagement.Domain.Constants;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Utils;

namespace VMMCStockManagement.Web.Utils
{
	public class WebSecurity : IWebSecurity
	{
		IHttpContextAccessor httpContextAccessor;
		private readonly UserManager<User> userManager;
		private readonly RoleManager<Role> roleManager;
		private readonly IConfiguration configuration;
		public WebSecurity(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager,
			RoleManager<Role> roleManager, IConfiguration configuration)
		{
			this.httpContextAccessor = httpContextAccessor;
			this.userManager = userManager;
			this.roleManager = roleManager;
			this.configuration = configuration;
		}

		public string UserId => httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

		public string Role => GetRole().Result;

		private async Task<string> GetRole()
		{
			var u = User;
			var roles = await userManager.GetRolesAsync(User);

			var role = roles.FirstOrDefault();

			return string.IsNullOrEmpty(role) ? RoleConstants.Requester : role;
		}
		public User User { get { return (userManager.Users.FirstOrDefault(x => x.Id == UserId)); } }

		public string DomainUrl => configuration.GetValue<string>("SystemConfig:URL");

		public bool HasRole(string role)
		{
			var iss = userManager.IsInRoleAsync(User, role).Result;
			return iss;
		}

		public bool InRole(string role)
		{
			throw new NotImplementedException();
		}
	}
}
