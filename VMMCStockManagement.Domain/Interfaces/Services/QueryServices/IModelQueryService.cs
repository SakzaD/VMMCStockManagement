using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.Model;
using VMMCStockManagement.Domain.Models.Responses;

namespace VMMCStockManagement.Domain.Interfaces.Services.QueryServices
{
	public interface IModelQueryService : IBaseQueryService<ModelFilter, Model>
	{
		ObjectListResponse<ModelListResponse> Filter(ModelFilter filter);
		ObjectResponse<ModelResponse> Get(long id);
	}
}
