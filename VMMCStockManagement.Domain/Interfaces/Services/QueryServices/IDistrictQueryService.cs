using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.District;
using VMMCStockManagement.Domain.Models.Responses;

namespace VMMCStockManagement.Domain.Interfaces.Services.QueryServices
{
	public interface IDistrictQueryService : IBaseQueryService<DistrictFilter, District>
	{
		ObjectListResponse<DistrictListResponse> Filter(DistrictFilter filter);
		ObjectResponse<DistrictResponse> Get(long id);
	}
}
