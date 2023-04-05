using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.Department;
using VMMCStockManagement.Domain.Models.Responses;

namespace VMMCStockManagement.Domain.Interfaces.Services.QueryServices
{
	public interface IDepartmentQueryService : IBaseQueryService<DepartmentFilter, Department>
	{
		ObjectListResponse<DepartmentListResponse> Filter(DepartmentFilter filter);
		ObjectResponse<DepartmentResponse> Get(long id);
	}
}
