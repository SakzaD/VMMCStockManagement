using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VMMCStockManagement.Domain.Interfaces.Services.CommandServices;
using VMMCStockManagement.Domain.Models.Requests;

namespace VMMCStockManagement.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReasonController : ControllerBase
	{
		private readonly IReasonCommandService reasonCommandService;
		public ReasonController(IReasonCommandService reasonCommandService)
		{
			this.reasonCommandService = reasonCommandService;
		}

		[HttpPost("create")]
		public IActionResult CreateAsset([FromBody] ReasonRequest request)
		{
			var res = reasonCommandService.Add(request);
			return Ok(res);
		}

		[HttpPost("delete")]
		public IActionResult DeleteAsset([FromBody] BaseRequest request)
		{
			var res = reasonCommandService.Delete(request);
			return Ok(res);
		}

		[HttpPost("update")]
		public IActionResult UpdateAsset([FromBody] ReasonRequest request)
		{
			var res = reasonCommandService.Edit(request);
			return Ok(res);
		}
	}
}
