using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Constants;
using VMMCStockManagement.Domain.Enums;
using VMMCStockManagement.Domain.Interfaces.Repositories;
using VMMCStockManagement.Domain.Models.GroupType;
using VMMCStockManagement.Domain.Models.Requests;
using VMMCStockManagement.Domain.Models.Responses.DashboardResponse;
using VMMCStockManagement.Domain.Models.Responses;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Interfaces.Services.QueryServices;

namespace VMMCStockManagement.Domain.Services.QueryServices
{
	public class DashboardQueryService : IDashboardQueryService
	{
		private readonly ILogger<DashboardQueryService> logger;
		private readonly IQueryRepository<StockRequest, StockRequestFilter> stockRequestQueryRepo;		
		private readonly IQueryRepository<Stock, StockFilter> stockQueryRepo;
		

		public DashboardQueryService(ILogger<DashboardQueryService> logger,
			IQueryRepository<StockRequest, StockRequestFilter> stockRequestQueryRepo,
			IQueryRepository<Stock, StockFilter> stockQueryRepo)
		{
			this.logger = logger;
			this.stockRequestQueryRepo = stockRequestQueryRepo;
			this.stockQueryRepo = stockQueryRepo;			
		}


		public ObjectResponse<DashboardStatsResponse> GetDashboardByRole(DashboardRequest request)
		{

			var response = new ObjectResponse<DashboardStatsResponse>();

			try
			{
				var totalRequest = stockRequestQueryRepo
					.GetAll()
					.Where(x => x.Status == EntityStatus.Active && x.CreatedBy == request.UserId)
					.Count();

				//var totalReturn = assetReturnQueryRepo
				//	.GetAll()
				//	.Where(x => x.Status == EntityStatus.Active && x.CreatedBy == request.UserId)
				//	.Count();


				var dashStats = new DashboardStatsResponse
				{
					TotalRequest = totalRequest,
					TotalReturn = 0,
					TotalMyAssets = 0
				};

				response.CodeStatus = ResponseStatus.Success;
				response.Data = dashStats;
			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message);
			}

			return response;
		}


		public ObjectResponse<DashboardStatsResponse> GetStats()
		{
			var response = new ObjectResponse<DashboardStatsResponse>();

			try
			{			
				var totalStock = stockQueryRepo.GetAll().Where(x => x.Status == EntityStatus.Active).Count();

				var dailyStats = new List<DailyReportResponse>();

				var now = DateTime.Now;

				var months = now.GetCurrentYearMonths()
					.OrderBy(x => x.Month);


				var stats = stockRequestQueryRepo.GetAll().Select(x => new
				{
					Year = x.CreatedAt.Year,
					Month = x.CreatedAt.Month,

				}).GroupBy(x => new
				{
					Year = x.Year,
					Month = x.Month,
				}).Select(x => new DashboardGrouping
				{
					Year = x.Key.Year,
					Month = x.Key.Month,
					Total = x.Count()
				});

				var query =
				from m in months
				join s in stats on new { m.Year, m.Month } equals new { s.Year, s.Month } into gj
				from s in gj.DefaultIfEmpty()
				select s ?? new DashboardGrouping
				{
					Year = m.Year,
					Month = m.Month,
					Total = 0
				};

				query = query.OrderBy(x => x.Year)
						 .OrderBy(x => x.Month);


				var monthlyStats = new List<MonthlyReportResponse>();


				foreach (var item in query)
				{
					monthlyStats.Add(new MonthlyReportResponse
					{
						Month = (new DateTime(item.Year, item.Month, 1)).ToString("MMM"),
						Total = item.Total.ToString().Replace(',', '.')
					});
				}

				var dashStats = new DashboardStatsResponse
				{
					TotalRequest = totalStock,
					TotalReturn = totalStock,
					TotalTransfer = totalStock,
					TotalRepair = totalStock,
					TotalDamageOrLoss = totalStock,
					TotalDisposal = totalStock,
					TotalReplacement = totalStock,
					TotalAllAssets = totalStock,
					DailyStats = dailyStats,
					MonthlyStats = monthlyStats
				};


				response.CodeStatus = ResponseStatus.Success;
				response.Data = dashStats;
			}
			catch (Exception ex)
			{
				response.CodeStatus = ResponseStatus.Fail;
				logger.LogError(ex.Message);

			}

			return response;
		}
	}
}
