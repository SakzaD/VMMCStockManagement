using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse;
using VMMCStockManagement.Domain.Models.Responses;
using VMMCStockManagement.Domain.Entities;

namespace VMMCStockManagement.Domain.Interfaces.Services.QueryServices
{
	public interface IReferenceQueryService : IBaseQueryService<ReferenceFilter, Reference>
	{
		ObjectListResponse<ReferenceResponse> Filter(ReferenceFilter filter);
		ObjectResponse<ReferenceResponse> Get(long id);
	}
}
