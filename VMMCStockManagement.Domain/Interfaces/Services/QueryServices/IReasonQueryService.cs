using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Responses;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.Reason;

namespace VMMCStockManagement.Domain.Interfaces.Services.QueryServices
{
	public interface IReasonQueryService : IBaseQueryService<ReasonFilter, Reason>
	{
		ObjectListResponse<ReasonResponse> Filter(ReasonFilter filter);
		ObjectResponse<ReasonResponse> Get(long id);
	}
}
