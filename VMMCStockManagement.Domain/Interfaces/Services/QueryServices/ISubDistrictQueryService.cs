using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.SubDistrict;
using VMMCStockManagement.Domain.Models.Responses;

namespace VMMCStockManagement.Domain.Interfaces.Services.QueryServices
{
	public interface ISubDistrictQueryService : IBaseQueryService<SubDistrictFilter, SubDistrict>
	{
		ObjectListResponse<SubDistrictListResponse> Filter(SubDistrictFilter filter);
		ObjectResponse<SubDistrictResponse> Get(long id);
	}
}
