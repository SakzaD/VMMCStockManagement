using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Responses;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.RequestApproval;

namespace VMMCStockManagement.Domain.Interfaces.Services.QueryServices
{
	public interface IRequestApprovalQueryService : IBaseQueryService<RequestApprovalFilter, RequestApproval>
	{
		ObjectListResponse<RequestApprovalListResponse> Filter(RequestApprovalFilter filter);
		ObjectResponse<RequestApprovalResponse> Get(long id);
	}
}
