using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VMMCStockManagement.Domain.Interfaces.Services.CommandServices;
using VMMCStockManagement.Domain.Models.Requests;

namespace VMMCStockManagement.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SupplierController : ControllerBase
	{
		private readonly ISupplierCommandService supplierCommandService;
		public SupplierController(ISupplierCommandService modelCommandService)
		{
			this.supplierCommandService = modelCommandService;
		}

		[HttpPost("create")]
		public IActionResult CreateModel([FromBody] SupplierRequest request)
		{
			var res = supplierCommandService.Add(request);
			return Ok(res);
		}

		[HttpPost("delete")]
		public IActionResult DeleteModel([FromBody] BaseRequest request)
		{
			var res = supplierCommandService.Delete(request);
			return Ok(res);
		}

		[HttpPost("update")]
		public IActionResult UpdateModel([FromBody] SupplierRequest request)
		{
			var res = supplierCommandService.Edit(request);
			return Ok(res);
		}
	}
}
