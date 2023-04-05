using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VMMCStockManagement.Domain.Interfaces.Services.CommandServices;
using VMMCStockManagement.Domain.Models.Requests;

namespace VMMCStockManagement.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryCommandService assetCategoryCommandService;
		public CategoryController(ICategoryCommandService assetCategoryCommandService)
		{
			this.assetCategoryCommandService = assetCategoryCommandService;
		}

		[HttpPost("create")]
		public IActionResult CreateAsset([FromBody] CategoryRequest request)
		{
			var res = assetCategoryCommandService.Add(request);
			return Ok(res);
		}

		[HttpPost("delete")]
		public IActionResult DeleteAsset([FromBody] BaseRequest request)
		{
			var res = assetCategoryCommandService.Delete(request);
			return Ok(res);
		}

		[HttpPost("update")]
		public IActionResult UpdateAsset([FromBody] CategoryRequest request)
		{
			var res = assetCategoryCommandService.Edit(request);
			return Ok(res);
		}
	}
}
