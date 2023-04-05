using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Interfaces.Repositories;
using VMMCStockManagement.Domain.Interfaces.Services.CommandServices;
using VMMCStockManagement.Domain.Models.Requests;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Responses;

namespace VMMCStockManagement.Domain.Services.CommandServices
{
	public class CategoryCommandService : BaseCommandService<Category>, ICategoryCommandService
	{

		private readonly IQueryRepository<Category, CategoryFilter> queryRepository;
		public CategoryCommandService(ICommandRepository<Category> commandRepository,
			ILogger<BaseCommandService<Category>> logger,
			IQueryRepository<Category, CategoryFilter> queryRepository) : base(commandRepository, logger)
		{
			this.queryRepository = queryRepository;
		}

		public override string ServiceName => nameof(CategoryCommandService);

		public BaseResponse Add(CategoryRequest request)
		{
			Category location = Category.Create(request);

			return Create(location);
		}

		public override void AfterCreation(Category entity)
		{

		}

		public BaseResponse Delete(BaseRequest request)
		{
			var district = queryRepository.GetById(request.Id.Value);

			if (district == null)
			{
				return new BaseResponse
				{
					CodeStatus = Enums.ResponseStatus.Fail,
					Message = "Invalid category selected"
				};
			}


			district.UpdatedAt = DateTime.Now;
			district.UpdatedBy = request.UserId;
			district.Status = Enums.EntityStatus.Deleted;
			var response = Delete(district);

			return response;
		}

		public BaseResponse Edit(CategoryRequest request)
		{
			var district = queryRepository.GetById(request.Id.Value);

			if (district == null)
				return new BaseResponse
				{
					CodeStatus = Enums.ResponseStatus.Fail,
					Message = "Invalid asset selected"
				};


			district.UpdatedAt = DateTime.Now;
			district.Name = request.Name;
			district.Description = request.Description;

			var response = Update(district);

			return response;
		}
	}
}
