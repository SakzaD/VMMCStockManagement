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
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.Stock;

namespace VMMCStockManagement.Domain.Services.QueryServices
{
	public class StockQueryService : BaseQueryService<StockFilter, Stock>, IStockQueryService
	{
		private readonly IQueryRepository<StockRequestAssetCategoryItem, StockRequestAssetCategoryItemFilter> assetRequestAssetCategoryItemQueryRepo;
		public StockQueryService(IQueryRepository<Stock, StockFilter> queryRepository,
			IQueryRepository<StockRequestAssetCategoryItem, StockRequestAssetCategoryItemFilter> assetRequestAssetCategoryItemqueryRepository,
			ILogger<BaseService> logger) : base(queryRepository, logger)
		{
			this.assetRequestAssetCategoryItemQueryRepo = assetRequestAssetCategoryItemqueryRepository;
		}

		public override string ServiceName => nameof(StockQueryService);
		public ObjectListResponse<StockListResponse> Filter(StockFilter filter)
		{
			var response = new ObjectListResponse<StockListResponse>();
			var data = queryRepository.Filter(filter).ToList(); var mappedData = new List<StockListResponse>(); foreach (var item in data)
			{
				string category = item.Category == null ? "Unknown" : item.Category.Name;
				string make = "Unknown";
				var model = item.Model;
				var modelStr = "Unknown";
				if (model != null)
				{
					modelStr = item.Model.Name;
					make = item.Model.Make.Name;
				}
				string grant = item.Grant == null ? "Unknown" : item.Grant.Name;
				mappedData.Add(new StockListResponse
				{
					Id = item.Id,
					Name = item.Name,
					Description = item.Description,
					SerialNumber = item.SerialNumber,
					RegistrationNumber = item.RegistrationNumber,
					IsAddedToRequest = item.IsAddedToRequest,
					ReferenceOrTicketNumber = item.ReferenceNumber,
					CategoryId = item.CategoryId,
					ModelId = item.ModelId,
					MakeId = item.Model.MakeId,
					GrantId = item.GrantId,
					Category = category,
					MakeName = make,
					ModelName = modelStr,
					GrantName = grant,
				});
			}
			response.Data = mappedData;
			response.CodeStatus = ResponseStatus.Success; return response;
		}
		public string GrantName { get; set; }
		public ObjectResponse<StockResponse> Get(long id)
		{
			var response = new ObjectResponse<StockResponse>();
			response.CodeStatus = ResponseStatus.Success;
			response.Message = "Item found.";
			var asset = queryRepository.GetById(id);

			var mappedData = new StockResponse
			{
				Id = asset.Id,
				Name = asset.Name,
				RegistrationNumber = asset.RegistrationNumber,
				SerialNumber = asset.SerialNumber,
				CategoryId = asset.CategoryId,
				MakeId = asset.Model.MakeId,
				MakeName = asset.Model.Name,
				ModelId = asset.ModelId,
				ModelName = asset.Model.Name,
				SupplierId = asset.SupplierId,
				warrantyType = asset.WarrantyType,
				Description = asset.Description,
				PurchasePrice = asset.PurchasePrice,
				PurchaseOrReceivedDate = asset.PurchaseOrReceivedDate == null ? "" : asset.PurchaseOrReceivedDate.Value.ToString("yyyy-MM-dd"),
				TransferDate = asset.TransferDate == null ? "" : asset.TransferDate.Value.ToString("yyyy-MM-dd"),
				IsAddedToRequest = asset.IsAddedToRequest,
				ReferenceOrTicketNumber = asset.ReferenceNumber,
				WarrantStartDate = asset.WarrantStartDate == null ? "" : asset.WarrantStartDate.Value.ToString("yyyy-MM-dd"),
				WarrantEndDate = asset.WarrantEndDate == null ? "" : asset.WarrantEndDate.Value.ToString("yyyy-MM-dd"),
				GrantId = asset.GrantId,
				LocationId = asset.FacilityId,
				DepartmentId = asset.DepartmentId,
				InvoiceNumber = asset.InvoiceNumber,

			};
			response.Data = mappedData;
			return response;
		}
		public ObjectResponse<StockResponse> GetScannedItem(StockFilter filter)
		{
			var response = new ObjectResponse<StockResponse>();

			if (string.IsNullOrEmpty(filter.DeviceIdentifier))
			{
				response.CodeStatus = ResponseStatus.Fail;
				response.Message = "Make sure you provide correct Serial Number/Registered number";
				return response;
			}

			var asset = queryRepository.GetAll()
				.FirstOrDefault(x => x.RegistrationNumber == filter.DeviceIdentifier.Trim());

			if (asset == null)
			{
				asset = queryRepository.GetAll()
				.FirstOrDefault(x => x.SerialNumber == filter.DeviceIdentifier.Trim());
			}

			if (asset == null)
			{
				response.CodeStatus = ResponseStatus.Fail;
				response.Message = "Item scanned does not exist on our system.";
				return response;
			}


			var existItem = assetRequestAssetCategoryItemQueryRepo
				.GetAll().FirstOrDefault(x => x.StockId == asset.Id);

			if (existItem != null)
			{
				response.CodeStatus = ResponseStatus.Fail;
				response.Message = "Item you scanned have already assigned to another user.";
				return response;
			}

			response.CodeStatus = ResponseStatus.Success;
			response.Message = "Item available.";
			var mappedData = FormatAssetEntity(asset);
			response.Data = mappedData;
			return response;

		}

		public ObjectResponse<StockResponse> GetDeviceBySerialAndAssetId(StockFilter filter)
		{
			var response = new ObjectResponse<StockResponse>();

			if (string.IsNullOrEmpty(filter.DeviceIdentifier))
			{
				response.CodeStatus = ResponseStatus.Fail;
				response.Message = "Provide device identifier.";
				return response;
			}

			var asset = queryRepository.Filter(filter)
				.FirstOrDefault();

			if (asset == null)
			{
				response.CodeStatus = ResponseStatus.Fail;
				response.Message = "No asset was found by this Serial/Asset Id.";
				return response;
			}


			response.CodeStatus = ResponseStatus.Success;
			response.Message = "Item available.";
			var mappedData = FormatAssetEntity(asset);
			response.Data = mappedData;
			return response;

		}
		private StockResponse FormatAssetEntity(Stock asset)
		{
			string category = asset.Category == null ? "Unknown" : asset.Category.Name;
			string make = "Unknown";
			var model = asset.Model;
			var modelStr = "Unknown";
			if (model != null)
			{
				modelStr = asset.Model.Name;
				make = asset.Model.Make.Name;

			}
			string grant = asset.Grant == null ? "Unknown" : asset.Grant.Name;

			return new StockResponse
			{
				Id = asset.Id,
				Name = asset.Name,
				Description = asset.Description,
				SerialNumber = asset.SerialNumber,
				RegistrationNumber = asset.RegistrationNumber,
				IsAddedToRequest = asset.IsAddedToRequest,
				ReferenceOrTicketNumber = asset.ReferenceNumber,
				CategoryId = asset.CategoryId,
				MakeId = asset.Model.MakeId,
				ModelId = asset.ModelId,
				GrantId = asset.GrantId,
				MakeName = make,
				ModelName = modelStr,

			};
		}
	}
}
