using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VMMCStockManagement.Domain.Interfaces.Services.CommandServices;
using VMMCStockManagement.Domain.Models.Requests;

namespace VMMCStockManagement.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MakeController : ControllerBase
	{
		private readonly IMakeCommandService makeCommandService;
		public MakeController(IMakeCommandService modelCommandService)
		{
			this.makeCommandService = modelCommandService;
		}

		[HttpPost("create")]
		public IActionResult CreateMake([FromBody] MakeRequest request)
		{
			var res = makeCommandService.Add(request);
			return Ok(res);
		}

		[HttpPost("delete")]
		public IActionResult DeleteMake([FromBody] BaseRequest request)
		{
			var res = makeCommandService.Delete(request);
			return Ok(res);
		}

		[HttpPost("update")]
		public IActionResult UpdateMake([FromBody] MakeRequest request)
		{
			var res = makeCommandService.Edit(request);
			return Ok(res);
		}
	}
}
