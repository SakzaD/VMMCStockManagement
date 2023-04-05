using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Interfaces.Services.QueryServices;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse;
using VMMCStockManagement.Domain.Models.Responses;

namespace VMMCStockManagement.Domain.Services.QueryServices
{
	public class ReportQueryService : IReportQueryService
	{
		//private readonly IUserAssetQueryService userAssetQueryService;
		private readonly ILogger<ReportQueryService> logger;
		public ReportQueryService(//IUserAssetQueryService userAssetQueryService,
								  ILogger<ReportQueryService> logger)
		{
			//this.userAssetQueryService = userAssetQueryService;
			this.logger = logger;

		}

		public ObjectListResponse<ReportListResponse> GenerateAssetReport(ReportFilter reportFilter)
		{
			var results = new ObjectListResponse<ReportListResponse>();

			var reportList = new List<ReportListResponse>();


			try
			{
				var now = DateTime.Now.AddDays(-25);
				reportList.Add(new ReportListResponse
				{
					RequestedDate = now.ToString("yyyy/MM/dd"),
					DateExpected = now.AddDays(10).ToString("yyyy/MM/dd"),
					DateAssigned = now.AddDays(15).ToString("yyyy/MM/dd"),
					AssetType = "Laptop",
					Department = "BSI",
					Location = "Centurion",
					AssinedTo = "Neo Mashego",

				});
				reportList.Add(new ReportListResponse
				{
					RequestedDate = now.ToString("yyyy/MM/dd"),
					DateExpected = now.AddDays(10).ToString("yyyy/MM/dd"),
					DateAssigned = now.AddDays(15).ToString("yyyy/MM/dd"),
					AssetType = "Laptop",
					Department = "BSI",
					Location = "Centurion",
					AssinedTo = "Neo Mashego",

				}); reportList.Add(new ReportListResponse
				{
					RequestedDate = now.ToString("yyyy/MM/dd"),
					DateExpected = now.AddDays(10).ToString("yyyy/MM/dd"),
					DateAssigned = now.AddDays(15).ToString("yyyy/MM/dd"),
					AssetType = "Laptop",
					Department = "BSI",
					Location = "Centurion",
					AssinedTo = "Neo Mashego",

				}); reportList.Add(new ReportListResponse
				{
					RequestedDate = now.ToString("yyyy/MM/dd"),
					DateExpected = now.AddDays(10).ToString("yyyy/MM/dd"),
					DateAssigned = now.AddDays(15).ToString("yyyy/MM/dd"),
					AssetType = "Laptop",
					Department = "BSI",
					Location = "Centurion",
					AssinedTo = "Neo Mashego",

				});

				results.Data = reportList;

				results.CodeStatus = Enums.ResponseStatus.Success;

			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message);

			}


			return results;
		}
	}
}
