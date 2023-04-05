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
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.StockRequestAssetCategory;

namespace VMMCStockManagement.Domain.Services.QueryServices
{
	public class StockRequestAssetCategoryQueryService : BaseQueryService<StockRequestAssetCategoryFilter, StockRequestAssetCategory>, IStockRequestAssetCategoryQueryService
	{

		public StockRequestAssetCategoryQueryService(IQueryRepository<StockRequestAssetCategory, StockRequestAssetCategoryFilter> queryRepository,
			ILogger<BaseService> logger) : base(queryRepository, logger)
		{

		}

		public override string ServiceName => nameof(StockRequestAssetCategoryQueryService);

		public ObjectListResponse<StockRequestAssetCategoryListResponse> Filter(StockRequestAssetCategoryFilter filter)
		{
			var response = new ObjectListResponse<StockRequestAssetCategoryListResponse>();
			var data = queryRepository.Filter(filter).ToList();

			var mappedData = new List<StockRequestAssetCategoryListResponse>();

			foreach (var item in data)
			{
				mappedData.Add(new StockRequestAssetCategoryListResponse
				{
					Id = item.Id,
					Name = item.Category.Name,
					Description = item.Category.Description,
					Qty = item.Qty
				});
			}
			response.Data = mappedData;
			response.CodeStatus = ResponseStatus.Success;

			return response;
		}

		public ObjectResponse<StockRequestAssetCategoryResponse> Get(long id)
		{
			throw new NotImplementedException();
		}
	}
}
