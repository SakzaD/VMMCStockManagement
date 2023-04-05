using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Enums;
using VMMCStockManagement.Domain.Interfaces.Repositories;
using VMMCStockManagement.Domain.Interfaces.Services.QueryServices;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Responses;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.Category;

namespace VMMCStockManagement.Domain.Services.QueryServices
{
	public class StockCategoryQueryService : BaseQueryService<CategoryFilter, Category>, ICategoryQueryService
	{

		public StockCategoryQueryService(IQueryRepository<Category, CategoryFilter> queryRepository,
			ILogger<BaseService> logger) : base(queryRepository, logger)
		{

		}

		public override string ServiceName => nameof(CategoryQueryService);

		public ObjectListResponse<CategoryListResponse> Filter(CategoryFilter filter)
		{
			var response = new ObjectListResponse<CategoryListResponse>();
			var data = queryRepository.Filter(filter).ToList();

			var mappedData = new List<CategoryListResponse>();

			foreach (var item in data)
			{
				mappedData.Add(new CategoryListResponse
				{
					Id = item.Id,
					Name = item.Name,
					Description = item.Description,
				});
			}
			response.Data = mappedData;
			response.CodeStatus = ResponseStatus.Success;

			return response;
		}

		public ObjectResponse<CategoryResponse> Get(long id)
		{
			throw new NotImplementedException();
		}
	}
}
