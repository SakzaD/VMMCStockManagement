using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Responses;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.Category;

namespace VMMCStockManagement.Domain.Interfaces.Services.QueryServices
{
	public interface ICategoryQueryService : IBaseQueryService<CategoryFilter, Category>
	{
		ObjectListResponse<CategoryListResponse> Filter(CategoryFilter filter);
		ObjectResponse<CategoryResponse> Get(long id);
	}
}
