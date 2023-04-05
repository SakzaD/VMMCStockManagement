using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VMMCStockManagement.Domain.Interfaces.Services.CommandServices;
using VMMCStockManagement.Domain.Models.Requests;

namespace VMMCStockManagement.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DepartmentController : ControllerBase
	{
		private readonly IDepartmentCommandService departmentCommandService;
		public DepartmentController(IDepartmentCommandService departmentCommandService)
		{
			this.departmentCommandService = departmentCommandService;
		}

		[HttpPost("create")]
		public IActionResult CreateDepartment([FromBody] DepartmentRequest request)
		{
			var res = departmentCommandService.Add(request);
			return Ok(res);
		}

		[HttpPost("delete")]
		public IActionResult DeleteDepartment([FromBody] BaseRequest request)
		{
			var res = departmentCommandService.Delete(request);
			return Ok(res);
		}

		[HttpPost("update")]
		public IActionResult UpdateDepartment([FromBody] DepartmentRequest request)
		{
			var res = departmentCommandService.Edit(request);
			return Ok(res);
		}
	}
}
