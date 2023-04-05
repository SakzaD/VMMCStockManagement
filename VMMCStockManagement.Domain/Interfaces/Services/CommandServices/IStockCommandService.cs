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
	public interface IStockCommandService : IBaseCommandService<Stock>
	{
		BaseResponse Add(StockEntityRequest request);
		BaseResponse Edit(StockEntityRequest request);
		BaseResponse Delete(BaseRequest request);
		Task<BaseResponse> BulkUploadAsync(FileUploadRequest request);
		BaseResponse BulkUpload(FileUploadRequest request);
	}
}
