using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.Grant;
using VMMCStockManagement.Domain.Models.Responses;

namespace VMMCStockManagement.Domain.Interfaces.Services.QueryServices
{
	public interface IGrantQueryService : IBaseQueryService<GrantFilter, Grant>
	{
		ObjectListResponse<GrantListResponse> Filter(GrantFilter filter);
		ObjectResponse<GrantResponse> Get(long id);
	}
}
