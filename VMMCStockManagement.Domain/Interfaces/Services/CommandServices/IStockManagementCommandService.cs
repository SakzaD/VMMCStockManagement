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
	public interface IStockManagementCommandService : IBaseCommandService<Stock>
	{
		BaseResponse Add(StockTrackRequest request);
		BaseResponse Edit(StockTrackRequest request);
		BaseResponse Delete(BaseRequest request);
	}
}
