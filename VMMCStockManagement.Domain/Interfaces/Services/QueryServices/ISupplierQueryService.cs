using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.Supplier;
using VMMCStockManagement.Domain.Models.Responses;

namespace VMMCStockManagement.Domain.Interfaces.Services.QueryServices
{
	public interface ISupplierQueryService : IBaseQueryService<SupplierFilter, Supplier>
	{
		ObjectListResponse<SupplierListResponse> Filter(SupplierFilter filter);
		ObjectResponse<SupplierResponse> Get(long id);
	}
}
