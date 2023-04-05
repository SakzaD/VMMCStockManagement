using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Responses;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.Stock;

namespace VMMCStockManagement.Domain.Interfaces.Services.QueryServices
{
	public interface IStockQueryService : IBaseQueryService<StockFilter, Stock>
	{
		ObjectListResponse<StockListResponse> Filter(StockFilter filter);
		ObjectResponse<StockResponse> Get(long id);
		ObjectResponse<StockResponse> GetScannedItem(StockFilter filter);
		ObjectResponse<StockResponse> GetDeviceBySerialAndAssetId(StockFilter filter);
	}
}
