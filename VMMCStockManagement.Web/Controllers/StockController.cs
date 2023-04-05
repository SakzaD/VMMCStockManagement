using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VMMCStockManagement.Domain.Interfaces.Services.CommandServices;
using VMMCStockManagement.Domain.Interfaces.Services.QueryServices;
using VMMCStockManagement.Domain.Models.Requests;
using VMMCStockManagement.Domain.Models.Requests.Filters;

namespace VMMCStockManagement.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StockController : ControllerBase
	{
		private readonly IStockCommandService assetCommandService;
		private readonly IStockQueryService assetQueryService;
		public StockController(IStockCommandService assetCommandService,
			IStockQueryService assetQueryService)
		{
			this.assetCommandService = assetCommandService;
			this.assetQueryService = assetQueryService;
		}

		[HttpPost("create")]
		public IActionResult CreateAsset([FromBody] StockEntityRequest request)
		{
			var res = assetCommandService.Add(request);
			return Ok(res);
		}

		[HttpPost("upload-bulk")]
		public async Task<IActionResult> BulkUpload([FromForm] FileUploadRequest request)
		{
			var res = await assetCommandService.BulkUploadAsync(request);
			return Ok(res);
		}

		[HttpGet("get-by-id")]
		public IActionResult GetById(long id)
		{
			var res = assetQueryService.Get(id);
			return Ok(res);
		}


		[HttpPost("delete")]
		public IActionResult DeleteAsset([FromBody] BaseRequest request)
		{
			var res = assetCommandService.Delete(request);
			return Ok(res);
		}

		[HttpPost("update")]
		public IActionResult UpdateAsset([FromBody] StockEntityRequest request)
		{
			var res = assetCommandService.Edit(request);
			return Ok(res);
		}

		[HttpGet("get-by-number")]
		public IActionResult GetByCode([FromQuery] StockFilter filter)
		{
			var res = assetQueryService.GetScannedItem(filter);
			return Ok(res);
		}

		[HttpGet("get-by-device-id")]
		public IActionResult GetByDeviceIdentifier([FromQuery] StockFilter filter)
		{
			var res = assetQueryService.Filter(filter);
			return Ok(res);
		}

		[HttpGet("search-by-device-id")]
		public IActionResult GetDeviceBySerialAndId([FromQuery] StockFilter filter)
		{
			var res = assetQueryService.GetDeviceBySerialAndAssetId(filter);
			return Ok(res);
		}
	}
}
