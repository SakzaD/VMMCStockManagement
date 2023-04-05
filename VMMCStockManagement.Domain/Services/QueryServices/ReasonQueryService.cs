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
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.Reason;
using VMMCStockManagement.Domain.Models.Responses;

namespace VMMCStockManagement.Domain.Services.QueryServices
{
	public class ReasonQueryService : BaseQueryService<ReasonFilter, Reason>, IReasonQueryService
	{

		public ReasonQueryService(IQueryRepository<Reason, ReasonFilter> queryRepository,
			ILogger<BaseService> logger) : base(queryRepository, logger)
		{

		}

		public override string ServiceName => nameof(ReasonQueryService);

		public ObjectListResponse<ReasonResponse> Filter(ReasonFilter filter)
		{
			var response = new ObjectListResponse<ReasonResponse>();
			var data = queryRepository.Filter(filter).ToList();

			var mappedData = new List<ReasonResponse>();

			foreach (var item in data)
			{
				mappedData.Add(new ReasonResponse
				{
					Id = item.Id,
					Description = item.Description,
					Category = item.ReasonCategory.Description,
					Type = item.LookupType.ToString(),

				});
			}
			response.Data = mappedData;
			response.CodeStatus = ResponseStatus.Success;

			return response;
		}

		public ObjectResponse<ReasonResponse> Get(long id)
		{
			throw new NotImplementedException();
		}
	}
}
