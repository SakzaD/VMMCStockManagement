using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VMMCStockManagement.Domain.Interfaces.Services.CommandServices;
using VMMCStockManagement.Domain.Models.Requests;

namespace VMMCStockManagement.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReasonCategoryController : ControllerBase
	{
		private readonly IReasonCategoryCommandService reasonCategoryCommandService;

		public ReasonCategoryController(IReasonCategoryCommandService reasonCategoryCommandService)
		{
			this.reasonCategoryCommandService = reasonCategoryCommandService;

		}


		[HttpPost("create")]
		public IActionResult Create([FromBody] ReasonCategoryRequest request)
		{
			var res = reasonCategoryCommandService.Add(request);
			return Ok(res);
		}

		[HttpPost("delete")]
		public IActionResult Delete([FromBody] BaseRequest request)
		{
			var res = reasonCategoryCommandService.Delete(request);
			return Ok(res);
		}

		[HttpPost("update")]
		public IActionResult UpdateDistrict([FromBody] ReasonCategoryRequest request)
		{
			var res = reasonCategoryCommandService.Edit(request);
			return Ok(res);
		}
	}
}
