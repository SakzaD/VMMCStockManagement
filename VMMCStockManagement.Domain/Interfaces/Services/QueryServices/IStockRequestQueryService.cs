using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Enums;
using VMMCStockManagement.Domain.Models.Requests;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Responses;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.RequestStock;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.Stock;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.StockRequest;

namespace VMMCStockManagement.Domain.Interfaces.Services.QueryServices
{
	public interface IStockRequestQueryService : IBaseQueryService<StockRequestFilter, StockRequest>
	{
		ObjectListResponse<StockRequestListResponse> Filter(StockRequestFilter filter);
		ObjectResponse<StockRequestResponse> Get(StockRequestFilter filter);
		ObjectListResponse<StockRequestManagerListResponse> GetAllNoneCompletedRequests();
		ObjectResponse<StockGrantResponse> GetGrantByReference(string? referenceNumber);
		ObjectResponse<StockRequestByIdResponse> GetRequestById(long id);
		ObjectResponse<NotificationResponse> GetRequestNumber(StockRequestFilter filter);
		StockStatus GetRequestStatus(long requestId);
		ObjectResponse<StockRequestResponse> SearchRequestByReferenceNumber(string referenceNumber);
		ObjectResponse<StockRequestManagerResponse> GetNoneCompletedRequestById(long requestId);
	}
}
