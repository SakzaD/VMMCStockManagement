using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VMMCStockManagement.Domain.Interfaces.Services.CommandServices;
using VMMCStockManagement.Domain.Interfaces.Services.QueryServices;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Requests;

namespace VMMCStockManagement.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserAssetController : ControllerBase
	{
		private readonly IUserAssetCommandService userAssetCommandService;
		private readonly IUserAssetQueryService userAssetQueryService;
		public UserAssetController(IUserAssetCommandService userAssetCommandService, IUserAssetQueryService userAssetQueryService
			//IAssetReturnQueryService assetReturnQueryService
			)
		{
			this.userAssetCommandService = userAssetCommandService;
			this.userAssetQueryService = userAssetQueryService;
		}

		[HttpPost("create")]
		public IActionResult CreateUserAsset([FromBody] UserAssetRequest request)
		{
			var res = userAssetCommandService.Add(request);
			return Ok(res);
		}

		[HttpPost("update")]
		public IActionResult UpdateUserAsset([FromBody] UserAssetRequest request)
		{
			var res = userAssetCommandService.Edit(request);
			return Ok(res);
		}

		[HttpGet("filter")]
		public IActionResult Filter([FromQuery] UserAssetFilter filter)
		{
			var res = userAssetQueryService.Filter(filter);
			return Ok(res);
		}


		[HttpGet("get-by-serial-registration")]
		public IActionResult GetAssetBySerialRegistrationNumber([FromQuery] UserAssetFilter filter)
		{
			//var res = assetReturnQueryService.Filter(filter);
			var res = userAssetQueryService.GetAssetBySerialRegistration(filter);
			return Ok(res);
		}

		[HttpGet("get-by-asset-id")]
		public IActionResult GetAssetById([FromQuery] UserAssetFilter filter)
		{
			//var res = assetReturnQueryService.Filter(filter);
			var res = userAssetQueryService.GetAssetById(filter);
			return Ok(res);
		}
	}
}
