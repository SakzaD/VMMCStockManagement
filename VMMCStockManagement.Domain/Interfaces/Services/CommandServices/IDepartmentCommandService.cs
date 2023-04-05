using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Models.Requests;
using VMMCStockManagement.Domain.Models.Responses;

namespace VMMCStockManagement.Domain.Interfaces.Services.CommandServices
{
	public interface IDepartmentCommandService : IBaseCommandService<Department>
	{
		BaseResponse Add(DepartmentRequest request);
		BaseResponse Edit(DepartmentRequest request);
		BaseResponse Delete(BaseRequest request);
	}
}
