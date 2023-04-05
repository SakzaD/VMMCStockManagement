using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.Facility;
using VMMCStockManagement.Domain.Models.Responses;

namespace VMMCStockManagement.Domain.Interfaces.Services.QueryServices
{
	public interface IFacilityQueryService : IBaseQueryService<FacilityFilter, Facility>
	{
		ObjectListResponse<FacilityListResponse> Filter(FacilityFilter filter);
		ObjectResponse<FacilityResponse> Get(long id);
	}
}
