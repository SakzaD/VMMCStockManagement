using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Responses;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.ReasonCategory;

namespace VMMCStockManagement.Domain.Interfaces.Services.QueryServices
{
	public interface IReasonCategoryQueryService : IBaseQueryService<ReasonCategoryFilter, ReasonCategory>
	{
		ObjectListResponse<ReasonCategoryListResponse> Filter(ReasonCategoryFilter filter);
		ObjectResponse<ReasonCategoryResponse> Get(long id);
	}
}
