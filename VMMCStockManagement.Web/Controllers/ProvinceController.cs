using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VMMCStockManagement.Domain.Interfaces.Services.CommandServices;
using VMMCStockManagement.Domain.Models.Requests;

namespace VMMCStockManagement.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProvinceController : ControllerBase
	{
		private readonly IProvinceCommandService provinceCommandService;
		public ProvinceController(IProvinceCommandService provinceCommandService)
		{
			this.provinceCommandService = provinceCommandService;
		}

		[HttpPost("create")]
		public IActionResult Create([FromBody] ProvinceRequest request)
		{
			var res = provinceCommandService.Add(request);
			return Ok(res);
		}

		[HttpPost("delete")]
		public IActionResult Delete([FromBody] BaseRequest request)
		{
			var res = provinceCommandService.Delete(request);
			return Ok(res);
		}

		[HttpPost("update")]
		public IActionResult Update([FromBody] ProvinceRequest request)
		{
			var res = provinceCommandService.Edit(request);
			return Ok(res);
		}
	}
}
