using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VMMCStockManagement.Domain.Interfaces;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Requests;

namespace VMMCStockManagement.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthenticationController : ControllerBase
	{
		private readonly IAuthenticateService authenticateService;
		public AuthenticationController(IAuthenticateService authenticateService)
		{
			this.authenticateService = authenticateService;
		}

		[HttpPost("create-account")]
		public async Task<IActionResult> CreateAccount([FromBody] UserRequest request)
		{
			var res = await authenticateService.Register(request);
			return Ok(res);
		}

		[HttpPost("update-account")]
		public async Task<IActionResult> UpdateUser([FromBody] UserRequest request)
		{
			var res = await authenticateService.Update(request);
			return Ok(res);
		}


		[HttpGet("filter")]
		public IActionResult Filter([FromQuery] UserFilter request)
		{
			var res = authenticateService.Filter(request);
			return Ok(res);
		}

		[HttpPost("create-role")]
		public async Task<IActionResult> AddRole([FromBody] RoleRequest request)
		{
			var res = await authenticateService.CreateNewRole(request);
			return Ok(res);
		}

		[HttpPost("add-user-role")]
		public async Task<IActionResult> AddUserToRole([FromBody] RoleAddRequest request)
		{
			var res = await authenticateService.AddUserToRole(request);
			return Ok(res);
		}

		[HttpPost("remove-role")]
		public async Task<IActionResult> RemoveRole([FromBody] RoleDeleteRequest request)
		{
			var res = await authenticateService.RemoveUserRole(request);
			return Ok(res);
		}

		[HttpGet("user/{userId}")]
		public async Task<IActionResult> GetUserById([FromQuery] string userId)
		{
			var response = await authenticateService.GetUserById(userId);
			return Ok(response);
		}
	}
}
