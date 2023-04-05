using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Interfaces.Repositories;
using VMMCStockManagement.Domain.Interfaces.Services.CommandServices;
using VMMCStockManagement.Domain.Interfaces.Services;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Requests;
using VMMCStockManagement.Domain.Models.Responses;

namespace VMMCStockManagement.Domain.Services.CommandServices
{
	public class ModelCommandService : BaseCommandService<Model>, IModelCommandService
	{

		private readonly IQueryRepository<Model, ModelFilter> queryRepository;
		public ModelCommandService(ICommandRepository<Model> commandRepository,
			ILogger<BaseCommandService<Model>> logger,
			IQueryRepository<Model, ModelFilter> queryRepository) : base(commandRepository, logger)
		{
			this.queryRepository = queryRepository;
		}

		public override string ServiceName => nameof(ModelCommandService);

		public BaseResponse Add(ModelRequest request)
		{
			Model model = Model.Create(request);

			return Create(model);
		}

		public override void AfterCreation(Model entity)
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
					Message = "Invalid model selected"
				};
			}


			district.UpdatedAt = DateTime.Now;
			district.UpdatedBy = request.UserId;
			district.Status = Enums.EntityStatus.Deleted;
			var response = Delete(district);

			return response;
		}

		public BaseResponse Edit(ModelRequest request)
		{
			var jobTitle = queryRepository.GetById(request.Id.Value);

			if (jobTitle == null)
				return new BaseResponse
				{
					CodeStatus = Enums.ResponseStatus.Fail,
					Message = "Invalid model selected"
				};
			jobTitle.Name = request.Name;
			jobTitle.MakeId = request.MakeId;
			jobTitle.UpdatedAt = DateTime.Now;
			jobTitle.UpdatedBy = request.UserId;

			var response = Update(jobTitle);

			return response;
		}

	}
}
