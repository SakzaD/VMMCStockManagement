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
	public interface IStockRequestAssetCategoryCommandService : IBaseCommandService<StockRequestAssetCategory>
	{
		BaseResponse Add(StockRequestAssetCategoryRequest request);
		BaseResponse Edit(StockRequestAssetCategoryRequest request);
		BaseResponse Delete(BaseRequest request);
	}
}
