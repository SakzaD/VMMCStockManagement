using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VMMCStockManagement.Domain.Interfaces.Services.CommandServices;
using VMMCStockManagement.Domain.Models.Requests;

namespace VMMCStockManagement.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class JobTitleController : ControllerBase
	{
		private readonly IJobTitleCommandService jobTitleCommandService;
		public JobTitleController(IJobTitleCommandService jobTitleCommandService)
		{
			this.jobTitleCommandService = jobTitleCommandService;
		}

		[HttpPost("create")]
		public IActionResult CreateJobTitle([FromBody] JobTitleRequest request)
		{
			var res = jobTitleCommandService.Add(request);
			return Ok(res);
		}

		[HttpPost("delete")]
		public IActionResult DeleteJobTitle([FromBody] BaseRequest request)
		{
			var res = jobTitleCommandService.Delete(request);
			return Ok(res);
		}

		[HttpPost("update")]
		public IActionResult UpdateJobTitle([FromBody] JobTitleRequest request)
		{
			var res = jobTitleCommandService.Edit(request);
			return Ok(res);
		}
	}
}
