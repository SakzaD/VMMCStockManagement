using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Interfaces.Repositories;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Requests;
using VMMCStockManagement.Domain.Models.Responses;
using VMMCStockManagement.Domain.Interfaces.Services.CommandServices;

namespace VMMCStockManagement.Domain.Services.CommandServices
{
	public class ReasonCategoryCommandService : BaseCommandService<ReasonCategory>, IReasonCategoryCommandService
	{
		private readonly IQueryRepository<ReasonCategory, ReasonCategoryFilter> queryRepository;
		public ReasonCategoryCommandService(ICommandRepository<ReasonCategory> commandRepository,
			ILogger<BaseCommandService<ReasonCategory>> logger, IQueryRepository<ReasonCategory, ReasonCategoryFilter> queryRepository) : base(commandRepository, logger)
		{
			this.queryRepository = queryRepository;
		}

		public override string ServiceName => nameof(CountryCommandService);

		public BaseResponse Add(ReasonCategoryRequest request)
		{
			ReasonCategory reasonCategory = ReasonCategory
				.Create(request);

			return Create(reasonCategory);
		}

		public override void AfterCreation(ReasonCategory entity)
		{

		}

		public BaseResponse Delete(BaseRequest request)
		{
			var country = queryRepository.GetById(request.Id.Value);

			if (country == null)
				return new BaseResponse
				{
					CodeStatus = Enums.ResponseStatus.Fail,
					Message = "Invalid country selected"
				};

			country.UpdatedAt = DateTime.Now;
			country.UpdatedBy = request.UserId;
			var response = Delete(country);

			return response;
		}

		public BaseResponse Edit(ReasonCategoryRequest request)
		{
			var country = queryRepository.GetById(request.Id.Value);

			if (country == null)
				return new BaseResponse
				{
					CodeStatus = Enums.ResponseStatus.Fail,
					Message = "Invalid country selected"
				};

			country.UpdatedAt = DateTime.Now;
			country.UpdatedBy = request.UserId;
			country.Description = request.Description;
			var response = Update(country);

			return response;
		}
	}
}
