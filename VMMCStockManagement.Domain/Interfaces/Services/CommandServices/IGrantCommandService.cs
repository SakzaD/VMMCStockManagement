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
	public interface IGrantCommandService : IBaseCommandService<Grant>
	{
		BaseResponse Add(GrantRequest request);
		BaseResponse Edit(GrantRequest request);
		BaseResponse Delete(BaseRequest request);
	}
}
