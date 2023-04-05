using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Enums;
using VMMCStockManagement.Domain.Interfaces.Services.QueryServices;
using VMMCStockManagement.Domain.Models.Requests;
using VMMCStockManagement.Domain.Models.Responses.DashboardResponse;

namespace VMMCStockManagement.Web.Pages
{
	[Authorize]
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
		private readonly IDashboardQueryService dashboardQueryService;
		private readonly UserManager<User> userManager;

		public DashboardStatsResponse DashboardStatsResponse { get; set; } = new DashboardStatsResponse();

		public IndexModel(ILogger<IndexModel> logger, IDashboardQueryService dashboardQueryService,
			UserManager<User> userManager)
		{
			_logger = logger;
			this.dashboardQueryService = dashboardQueryService;
			this.userManager = userManager;
		}

		public async Task<IActionResult> OnGet()
		{
			var user = await userManager.FindByNameAsync(User?.Identity?.Name);

			var dashboardRequest = new DashboardRequest
			{
				UserId = user.Id
			};

			var response = dashboardQueryService.GetDashboardByRole(dashboardRequest);

			if (response.CodeStatus == ResponseStatus.Success)
			{
				DashboardStatsResponse = response.Data;
			}

			return Page();
		}
	}
}