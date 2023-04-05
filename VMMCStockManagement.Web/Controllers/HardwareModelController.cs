using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VMMCStockManagement.Domain.Interfaces.Services.CommandServices;
using VMMCStockManagement.Domain.Models.Requests;

namespace VMMCStockManagement.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HardwareModelController : ControllerBase
	{
		private readonly IHardwareModelCommandService hardwareModelCommandService;
		public HardwareModelController(IHardwareModelCommandService hardwareModelCommandService)
		{
			this.hardwareModelCommandService = hardwareModelCommandService;
		}

		[HttpPost("create")]
		public IActionResult CreateAsset([FromBody] HardwareModelRequest request)
		{
			var res = hardwareModelCommandService.Add(request);
			return Ok(res);
		}

		[HttpPost("delete")]
		public IActionResult DeleteAsset([FromBody] BaseRequest request)
		{
			var res = hardwareModelCommandService.Delete(request);
			return Ok(res);
		}

		[HttpPost("update")]
		public IActionResult UpdateAsset([FromBody] HardwareModelRequest request)
		{
			var res = hardwareModelCommandService.Edit(request);
			return Ok(res);
		}
	}
}
