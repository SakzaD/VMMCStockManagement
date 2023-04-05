using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VMMCStockManagement.Domain.Interfaces.Services.CommandServices;
using VMMCStockManagement.Domain.Models.Requests;

namespace VMMCStockManagement.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LocationController : ControllerBase
	{
		private readonly ILocationCommandService locationCommandService;
		public LocationController(ILocationCommandService locationCommandService)
		{
			this.locationCommandService = locationCommandService;
		}

		[HttpPost("create")]
		public IActionResult CreateLocation([FromBody] LocationRequest request)
		{
			var res = locationCommandService.Add(request);
			return Ok(res);
		}

		[HttpPost("delete")]
		public IActionResult DeleteLocation([FromBody] BaseRequest request)
		{
			var res = locationCommandService.Delete(request);
			return Ok(res);
		}

		[HttpPost("update")]
		public IActionResult UpdateLocation([FromBody] LocationRequest request)
		{
			var res = locationCommandService.Edit(request);
			return Ok(res);
		}
	}
}
