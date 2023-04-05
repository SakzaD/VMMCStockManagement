using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VMMCStockManagement.Domain.Interfaces.Services.CommandServices;
using VMMCStockManagement.Domain.Models.Requests;

namespace VMMCStockManagement.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HardwareTypeController : ControllerBase
	{
		private readonly IHardwareTypeCommandService hardwareTypeCommandService;
		public HardwareTypeController(IHardwareTypeCommandService hardwareTypeCommandService)
		{
			this.hardwareTypeCommandService = hardwareTypeCommandService;
		}

		[HttpPost("create")]
		public IActionResult CreateAsset([FromBody] HardwareTypeRequest request)
		{
			var res = hardwareTypeCommandService.Add(request);
			return Ok(res);
		}

		[HttpPost("delete")]
		public IActionResult DeleteAsset([FromBody] BaseRequest request)
		{
			var res = hardwareTypeCommandService.Delete(request);
			return Ok(res);
		}

		[HttpPost("update")]
		public IActionResult UpdateAsset([FromBody] HardwareTypeRequest request)
		{
			var res = hardwareTypeCommandService.Edit(request);
			return Ok(res);
		}
	}
}
