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
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.UserAsset;
using VMMCStockManagement.Domain.Models.Responses;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse;

namespace VMMCStockManagement.Domain.Services.QueryServices
{
	public class UserAssetQueryService : BaseQueryService<UserAssetFilter, UserAsset>, IUserAssetQueryService
	{
		private readonly IQueryRepository<UserAsset, UserAssetFilter> userAssetQueryRepository;
		public UserAssetQueryService(IQueryRepository<UserAsset, UserAssetFilter> queryRepository,
		   IQueryRepository<UserAsset, UserAssetFilter> userAssetQueryRepository,

			ILogger<BaseService> logger) : base(queryRepository, logger)
		{
			this.userAssetQueryRepository = userAssetQueryRepository;

		}

		public override string ServiceName => nameof(UserAssetQueryService);

		public ObjectListResponse<UserAssetResponse> Filter(UserAssetFilter filter)
		{
			var response = new ObjectListResponse<UserAssetResponse>();
			var data = queryRepository.Filter(filter).ToList();

			var mappedData = new List<UserAssetResponse>();

			response.Data = mappedData;
			response.CodeStatus = ResponseStatus.Success;

			return response;
		}

		public ObjectResponse<UserAssetResponse> Get(long id)
		{
			var response = new ObjectResponse<UserAssetResponse>();
			response.CodeStatus = ResponseStatus.Success;
			response.Message = "Item found.";
			var userAsset = queryRepository.GetById(id);
			var mappedData = FormatAssetEntity(userAsset);
			response.Data = mappedData;
			return response;
		}

		public ObjectListResponse<SearchedAssetListResponse> GetAssetBySerialRegistration(UserAssetFilter filter)
		{
			var response = new ObjectListResponse<SearchedAssetListResponse>();

			try
			{

				//var assetFilter = new AssetFilter {
				//	SearchValue = filter.SearchValue,
				//};
				var assignedAssets = userAssetQueryRepository.Filter(filter).ToList();
				//var assets = assetQueryRepository.Filter(assetFilter);

				var foundAssets = new List<SearchedAssetListResponse>();

				foreach (var asset in assignedAssets)
				{
					foundAssets.Add(new SearchedAssetListResponse
					{
						Id = asset.AssetId.Value,
						Status = "Assigned",
						StatusDescription = "Assigned",
						DateAssigned = asset.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"),
						AssetHolder = asset.User.FullName,
						EmployeeNumber = asset.User.EmployeeNumber,
						RegistrationNumber = asset.Stock.RegistrationNumber,
						SerialNumber = asset.Stock.SerialNumber,
						Category = asset.Stock.Category.Name,
						IsReturned = asset.IsReturned
					});
				}

				response.Data = foundAssets;
				response.CodeStatus = ResponseStatus.Success;
			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message);
			}

			return response;
		}

		public ObjectResponse<UserAssetResponse> GetAssetById(UserAssetFilter filter)
		{
			var response = new ObjectResponse<UserAssetResponse>();

			try
			{


				var userAsset = userAssetQueryRepository.GetBy(filter.Id);


				var userAssetResponse = new UserAssetResponse
				{
					Id = userAsset.AssetId.Value,
					Make = userAsset.Stock.Model.Make.Name,
					Model = userAsset.Stock.Name,
					DateAssigned = userAsset.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"),
					AssetHolder = userAsset.User.FullName,
					RegistrationNumber = userAsset.Stock.RegistrationNumber,
					SerialNumber = userAsset.Stock.SerialNumber,
					Category = userAsset.Stock.Category.Name,
					AssetDescription = userAsset.Stock.Name,
				};

				response.Data = userAssetResponse;
				response.CodeStatus = ResponseStatus.Success;
			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message);
			}

			return response;
		}

		public ObjectResponse<UserAssetResponse> GetScannedItem(UserAssetFilter filter)
		{
			var response = new ObjectResponse<UserAssetResponse>();
			return response;

		}

		private UserAssetResponse FormatAssetEntity(UserAsset asset)
		{

			return new UserAssetResponse
			{
				Id = asset.Id,

			};
		}
	}
}
