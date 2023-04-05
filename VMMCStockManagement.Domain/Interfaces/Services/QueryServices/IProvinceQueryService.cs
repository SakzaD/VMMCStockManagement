using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.Province;
using VMMCStockManagement.Domain.Models.Responses;

namespace VMMCStockManagement.Domain.Interfaces.Services.QueryServices
{
	public interface IProvinceQueryService : IBaseQueryService<ProvinceFilter, Province>
	{
		ObjectListResponse<ProvinceListResponse> Filter(ProvinceFilter filter);
		ObjectResponse<ProvinceResponse> Get(long id);
	}
}
