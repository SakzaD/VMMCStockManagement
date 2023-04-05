using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VMMCStockManagement.Domain.Interfaces.Services.CommandServices;
using VMMCStockManagement.Domain.Models.Requests;

namespace VMMCStockManagement.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SubDistrictController : ControllerBase
	{
		private readonly ISubDistrictCommandService subDistrictCommandService;
		public SubDistrictController(ISubDistrictCommandService subDistrictCommandService)
		{
			this.subDistrictCommandService = subDistrictCommandService;
		}

		[HttpPost("create")]
		public IActionResult Create([FromBody] SubDistrictRequest request)
		{
			var res = subDistrictCommandService.Add(request);
			return Ok(res);
		}

		[HttpPost("delete")]
		public IActionResult Delete([FromBody] BaseRequest request)
		{
			var res = subDistrictCommandService.Delete(request);
			return Ok(res);
		}

		[HttpPost("update")]
		public IActionResult Update([FromBody] SubDistrictRequest request)
		{
			var res = subDistrictCommandService.Edit(request);
			return Ok(res);
		}
	}
}
