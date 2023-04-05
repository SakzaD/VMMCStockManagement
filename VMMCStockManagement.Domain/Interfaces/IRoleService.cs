using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Models.Requests;
using VMMCStockManagement.Domain.Models.Responses;

namespace VMMCStockManagement.Domain.Interfaces
{
	public interface IRoleService
	{
		ObjectListResponse<RoleResponse> Filter(RoleFilter filter);
		Task<ObjectListResponse<RoleResponse>> GetUserRoles(RoleFilter filter);
	}
}
