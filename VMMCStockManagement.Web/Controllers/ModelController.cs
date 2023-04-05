using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VMMCStockManagement.Domain.Interfaces.Services.CommandServices;
using VMMCStockManagement.Domain.Models.Requests;

namespace VMMCStockManagement.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ModelController : ControllerBase
	{
		private readonly IModelCommandService modelCommandService;
		public ModelController(IModelCommandService modelCommandService)
		{
			this.modelCommandService = modelCommandService;
		}

		[HttpPost("create")]
		public IActionResult CreateModel([FromBody] ModelRequest request)
		{
			var res = modelCommandService.Add(request);
			return Ok(res);
		}

		[HttpPost("delete")]
		public IActionResult DeleteModel([FromBody] BaseRequest request)
		{
			var res = modelCommandService.Delete(request);
			return Ok(res);
		}

		[HttpPost("update")]
		public IActionResult UpdateModel([FromBody] ModelRequest request)
		{
			var res = modelCommandService.Edit(request);
			return Ok(res);
		}
	}
}
