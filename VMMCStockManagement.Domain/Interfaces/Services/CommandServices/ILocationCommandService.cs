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
	public interface ILocationCommandService : IBaseCommandService<Location>
	{
		BaseResponse Add(LocationRequest request);
		BaseResponse Edit(LocationRequest request);
		BaseResponse Delete(BaseRequest request);
	}
}
