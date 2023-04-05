using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VMMCStockManagement.Domain.Interfaces.Services.CommandServices;
using VMMCStockManagement.Domain.Models.Requests;

namespace VMMCStockManagement.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReferenceController : ControllerBase
	{
		private readonly IReferenceCommandService referenceCommandService;
		public ReferenceController(IReferenceCommandService referenceCommandService)
		{
			this.referenceCommandService = referenceCommandService;
		}

		[HttpPost("generate-reference")]
		public IActionResult CreateAsset([FromBody] ReferenceRequest request)
		{
			var res = referenceCommandService.GenerateReference(request);
			return Ok(res);
		}
	}
}
