using VMMCStockManagement.Domain.Constants;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Interfaces.Services.QueryServices;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Services.QueryServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace VMMCStockManagement.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DashboardController : ControllerBase
	{
		private readonly IDashboardQueryService dashboardQueryService;
		private readonly IStockRequestQueryService assetRequestQueryService;
		private readonly UserManager<User> userManager;

		public DashboardController(IDashboardQueryService dashboardQueryService, UserManager<User> userManager,
			IStockRequestQueryService assetRequestQueryService)
		{
			this.dashboardQueryService = dashboardQueryService;
			this.assetRequestQueryService = assetRequestQueryService;
			this.userManager = userManager;
		}


		[HttpGet("stats")]
		public IActionResult FilterDepartment([FromQuery] DepartmentFilter filter)
		{
			var reponse = dashboardQueryService.GetStats();

			return Ok(reponse);
		}

		[HttpGet("notification")]
		public async Task<IActionResult> GetNotification([FromQuery] StockRequestFilter filter)
		{
			var user = await userManager.FindByNameAsync(User.Identity.Name);

			if (user == null)
				return NotFound();


			var userRoles = await userManager.GetRolesAsync(user);

			string? currentRole = userRoles.FirstOrDefault();

			var accessRole = currentRole.GetRoleEnum();


			filter.AccessRole = accessRole;

			var reponse = assetRequestQueryService.GetRequestNumber(filter);

			return Ok(reponse);
		}
	}
}
