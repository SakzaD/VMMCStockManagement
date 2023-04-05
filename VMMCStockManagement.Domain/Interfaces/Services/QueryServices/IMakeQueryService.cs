using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.Make;
using VMMCStockManagement.Domain.Models.Responses;

namespace VMMCStockManagement.Domain.Interfaces.Services.QueryServices
{
	public interface IMakeQueryService : IBaseQueryService<MakeFilter, Make>
	{
		ObjectListResponse<MakeListResponse> Filter(MakeFilter filter);
		ObjectResponse<MakeResponse> Get(long id);
	}
}
