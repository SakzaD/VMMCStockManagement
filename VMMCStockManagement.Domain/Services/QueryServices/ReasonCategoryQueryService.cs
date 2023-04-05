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
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.ReasonCategory;
using VMMCStockManagement.Domain.Models.Responses;

namespace VMMCStockManagement.Domain.Services.QueryServices
{
	public class ReasonCategoryQueryService : BaseQueryService<ReasonCategoryFilter, ReasonCategory>, IReasonCategoryQueryService
	{

		public ReasonCategoryQueryService(IQueryRepository<ReasonCategory, ReasonCategoryFilter> queryRepository,
			ILogger<BaseService> logger) : base(queryRepository, logger)
		{

		}

		public override string ServiceName => nameof(ReasonCategoryQueryService);

		public ObjectListResponse<ReasonCategoryListResponse> Filter(ReasonCategoryFilter filter)
		{
			var response = new ObjectListResponse<ReasonCategoryListResponse>();
			var data = queryRepository.Filter(filter).ToList();

			var mappedData = new List<ReasonCategoryListResponse>();

			foreach (var item in data)
			{
				mappedData.Add(new ReasonCategoryListResponse
				{
					Id = item.Id,
					Description = item.Description,

				});
			}
			response.Data = mappedData;
			response.CodeStatus = ResponseStatus.Success;

			return response;
		}

		public ObjectResponse<ReasonCategoryResponse> Get(long id)
		{
			var item = queryRepository.GetById(id);
			var response = new ObjectResponse<ReasonCategoryResponse>();
			response.CodeStatus = ResponseStatus.Success;

			response.Data = new ReasonCategoryResponse
			{
				Id = item.Id,
				Description = item.Description,

			};

			return response;
		}
	}
}
