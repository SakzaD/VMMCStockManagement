using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Responses;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.StockRequestAssetCategory;

namespace VMMCStockManagement.Domain.Interfaces.Services.QueryServices
{
	public interface IStockRequestAssetCategoryQueryService : IBaseQueryService<StockRequestAssetCategoryFilter, StockRequestAssetCategory>
	{
		ObjectListResponse<StockRequestAssetCategoryListResponse> Filter(StockRequestAssetCategoryFilter filter);
		ObjectResponse<StockRequestAssetCategoryResponse> Get(long id);
	}
}
