using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.StaffManager;
using VMMCStockManagement.Domain.Models.Responses;

namespace VMMCStockManagement.Domain.Interfaces.Services.QueryServices
{
	public interface IStaffManagerQueryService : IBaseQueryService<StaffManagerFilter, StaffManager>
	{
		ObjectListResponse<StaffManagerListResponse> Filter(StaffManagerFilter filter);
		ObjectResponse<StaffManagerResponse> Get(long id);
	}
}
