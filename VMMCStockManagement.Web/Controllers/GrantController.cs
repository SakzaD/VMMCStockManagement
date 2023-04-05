using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VMMCStockManagement.Domain.Interfaces.Services.CommandServices;
using VMMCStockManagement.Domain.Models.Requests;

namespace VMMCStockManagement.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GrantController : ControllerBase
	{
		private readonly IGrantCommandService grantCommandService;
		public GrantController(IGrantCommandService grantCommandService)
		{
			this.grantCommandService = grantCommandService;
		}

		[HttpPost("create")]
		public IActionResult CreateGrant([FromBody] GrantRequest request)
		{
			var res = grantCommandService.Add(request);
			return Ok(res);
		}

		[HttpPost("delete")]
		public IActionResult DeleteGrant([FromBody] BaseRequest request)
		{
			var res = grantCommandService.Delete(request);
			return Ok(res);
		}

		[HttpPost("update")]
		public IActionResult UpdateGrant([FromBody] GrantRequest request)
		{
			var res = grantCommandService.Edit(request);
			return Ok(res);
		}
	}
}
