using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VMMCStockManagement.Domain.Interfaces.Repositories;
using VMMCStockManagement.Domain.Interfaces;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Requests;

namespace VMMCStockManagement.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IAuthenticateService authenticateService;
		private readonly IUserQueryRepository userQueryRepository;
		public UserController(IAuthenticateService authenticateService,
			IUserQueryRepository userQueryRepository)
		{
			this.authenticateService = authenticateService;
			this.userQueryRepository = userQueryRepository;
		}

		[HttpPost("delete")]
		public async Task<IActionResult> Delete([FromBody] UserDeleteRequest request)
		{
			var res = await authenticateService.DeleteUser(request);
			return Ok(res);
		}


		[HttpPost("upload-users")]
		public async Task<IActionResult> UploadUsers([FromForm] UserUploadRequest request)
		{
			var res = await authenticateService.UploadUser(request);
			return Ok(res);
		}

		[HttpGet("search-employee")]
		public IActionResult Filter([FromQuery] UserAssetFilter filter)
		{
			var res = userQueryRepository.SearchByEmployeeNumber(filter.SearchValue);
			return Ok(res);
		}
	}
}
