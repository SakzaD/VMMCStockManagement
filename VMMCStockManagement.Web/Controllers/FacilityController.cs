using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VMMCStockManagement.Domain.Interfaces.Services.CommandServices;
using VMMCStockManagement.Domain.Models.Requests;

namespace VMMCStockManagement.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FacilityController : ControllerBase
	{
		private readonly IFacilityCommandService facilityCommandService;
		public FacilityController(IFacilityCommandService facilityCommandService)
		{
			this.facilityCommandService = facilityCommandService;
		}

		[HttpPost("create")]
		public IActionResult Create([FromBody] FacilityRequest request)
		{
			var res = facilityCommandService.Add(request);
			return Ok(res);
		}

		[HttpPost("delete")]
		public IActionResult Delete([FromBody] BaseRequest request)
		{
			var res = facilityCommandService.Delete(request);
			return Ok(res);
		}

		[HttpPost("update")]
		public IActionResult Update([FromBody] FacilityRequest request)
		{
			var res = facilityCommandService.Edit(request);
			return Ok(res);
		}
	}
}
