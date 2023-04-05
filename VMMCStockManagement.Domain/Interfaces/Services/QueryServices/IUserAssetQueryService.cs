using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Responses;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.UserAsset;

namespace VMMCStockManagement.Domain.Interfaces.Services.QueryServices
{
	public interface IUserAssetQueryService : IBaseQueryService<UserAssetFilter, UserAsset>
	{
		ObjectListResponse<UserAssetResponse> Filter(UserAssetFilter filter);
		ObjectResponse<UserAssetResponse> Get(long id);
		ObjectListResponse<SearchedAssetListResponse> GetAssetBySerialRegistration(UserAssetFilter filter);
		ObjectResponse<UserAssetResponse> GetAssetById(UserAssetFilter filter);
	}
}
