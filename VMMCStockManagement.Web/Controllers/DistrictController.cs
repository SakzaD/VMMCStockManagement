using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VMMCStockManagement.Domain.Interfaces.Services.CommandServices;
using VMMCStockManagement.Domain.Models.Requests;

namespace VMMCStockManagement.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DistrictController : ControllerBase
	{
		private readonly IDistrictCommandService districtCommandService;
		public DistrictController(IDistrictCommandService districtCommandService)
		{
			this.districtCommandService = districtCommandService;
		}

		[HttpPost("create")]
		public IActionResult Create([FromBody] DistrictRequest request)
		{
			var res = districtCommandService.Add(request);
			return Ok(res);
		}

		[HttpPost("delete")]
		public IActionResult Delete([FromBody] BaseRequest request)
		{
			var res = districtCommandService.Delete(request);
			return Ok(res);
		}

		[HttpPost("update")]
		public IActionResult UpdateDistrict([FromBody] DistrictRequest request)
		{
			var res = districtCommandService.Edit(request);
			return Ok(res);
		}
	}
}
