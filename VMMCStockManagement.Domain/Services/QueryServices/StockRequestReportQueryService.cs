using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Enums;
using VMMCStockManagement.Domain.Interfaces.Repositories;
using VMMCStockManagement.Domain.Interfaces.Services.QueryServices;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Responses;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse;

namespace VMMCStockManagement.Domain.Services.QueryServices
{
	public class StockRequestReportQueryService : BaseQueryService<StockRequestReportFilter, StockRequest>, IStockRequestReportQueryService
	{
		private readonly IQueryRepository<StockRequest, StockRequestReportFilter> queryRepository;
		private readonly UserManager<User> userManager;
		public StockRequestReportQueryService(IQueryRepository<StockRequest, StockRequestReportFilter> queryRepository,

			ILogger<BaseService> logger,
			UserManager<User> userManager) : base(queryRepository, logger)
		{
			this.userManager = userManager;
			this.queryRepository = queryRepository;
		}

		public override string ServiceName => nameof(StockRequestReportQueryService);

		public ObjectListResponse<StockRequestReportResponse> Filter(StockRequestReportFilter filter)
		{
			var response = new ObjectListResponse<StockRequestReportResponse>();
			var data = queryRepository.Filter(filter).ToList();

			var mappedData = new List<StockRequestReportResponse>();

			foreach (var item in data)
			{
				string grant = item.Grant == null ? "Unknown" : item.Grant.Name;
				mappedData.Add(new StockRequestReportResponse
				{
					Id = item.Id,
					RequesterId = item.RequesterId,
					RequesterName = item.Requester?.FullName,
					DepartmentId = item.DepartmentId,
					DepartmentName = item.Department?.Name,
					GrantId = item.GrantId,
					GrantName = grant,
					Status = item.RequestApproval.HOApproverStatus == StockStatus.Completed,
					DateRequested = item.CreatedAt.ToString("yyyy-MM-dd"),
					DateIssued = item.RequestApproval.HOApproverCompleteDate == null ? "Not completed" : item.RequestApproval.HOApproverCompleteDate.Value.ToString("yyyy-MM-dd"),
					DateRequired = item.DateRequired == null ? "N/A" : item.DateRequired.Value.ToString("yyyy-MM-dd"),
				});
			}
			response.Data = mappedData;
			response.CodeStatus = ResponseStatus.Success; return response;
		}

	}
}
