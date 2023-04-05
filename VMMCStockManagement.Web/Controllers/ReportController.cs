using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VMMCStockManagement.Domain.Interfaces.Services.QueryServices;
using VMMCStockManagement.Domain.Models.Requests.Filters;

namespace VMMCStockManagement.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReportController : ControllerBase
	{
		private readonly IStockRequestQueryService assetRequestQueryService;
		private readonly IStockQueryService assetQueryService;
		private readonly IStockRequestReportQueryService assetRequestReportQueryService;
		private readonly IReportQueryService reportQueryService;

		public ReportController(IStockRequestQueryService assetRequestQueryService,
			IStockQueryService assetQueryService, IStockRequestReportQueryService assetRequestReportQueryService,
			IReportQueryService reportQueryService)
		{
			this.assetRequestQueryService = assetRequestQueryService;
			this.assetQueryService = assetQueryService;
			this.assetRequestReportQueryService = assetRequestReportQueryService;
			this.reportQueryService = reportQueryService;
		}


		[HttpPost("stock-report")]
		public IActionResult CreateAsset([FromBody] StockFilter filter)
		{
			var res = assetQueryService.Filter(filter);
			return Ok(res);
		}


		[HttpPost("request-report")]
		public IActionResult CreateAsset([FromBody] StockRequestFilter filter)
		{
			var res = assetRequestQueryService.Filter(filter);
			return Ok(res);
		}

		[HttpGet("request-stock-report")]
		public IActionResult RequestAssetReport([FromQuery] StockRequestReportFilter filter)
		{
			return Ok(assetRequestReportQueryService.Filter(filter));
		}

		[HttpGet("generate-stock-report")]
		public IActionResult FilterAssetReport([FromQuery] ReportFilter filter)
		{
			var results = reportQueryService.GenerateAssetReport(filter);

			return Ok(results);
		}
	}
}
