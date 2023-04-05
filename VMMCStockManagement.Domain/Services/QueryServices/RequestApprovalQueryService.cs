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
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.RequestApproval;

namespace VMMCStockManagement.Domain.Services.QueryServices
{
	public class RequestApprovalQueryService : BaseQueryService<RequestApprovalFilter, RequestApproval>, IRequestApprovalQueryService
	{

		public RequestApprovalQueryService(IQueryRepository<RequestApproval, RequestApprovalFilter> queryRepository,
			ILogger<BaseService> logger) : base(queryRepository, logger)
		{

		}

		public override string ServiceName => nameof(RequestApprovalQueryService);

		public ObjectListResponse<RequestApprovalListResponse> Filter(RequestApprovalFilter filter)
		{
			var response = new ObjectListResponse<RequestApprovalListResponse>();
			var data = queryRepository.Filter(filter).ToList();

			var mappedData = new List<RequestApprovalListResponse>();

			foreach (var item in data)
			{
				mappedData.Add(new RequestApprovalListResponse
				{
					Id = item.Id,

				});
			}
			response.Data = mappedData;
			response.CodeStatus = ResponseStatus.Success;

			return response;
		}

		public ObjectResponse<RequestApprovalResponse> Get(long id)
		{
			throw new NotImplementedException();
		}
	}
}
