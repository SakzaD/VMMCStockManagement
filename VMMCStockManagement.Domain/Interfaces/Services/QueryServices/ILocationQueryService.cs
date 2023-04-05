using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.Location;
using VMMCStockManagement.Domain.Models.Responses;

namespace VMMCStockManagement.Domain.Interfaces.Services.QueryServices
{
	public interface ILocationQueryService : IBaseQueryService<LocationFilter, Location>
	{
		ObjectListResponse<LocationListResponse> Filter(LocationFilter filter);
		ObjectResponse<LocationResponse> Get(long id);
	}
}
