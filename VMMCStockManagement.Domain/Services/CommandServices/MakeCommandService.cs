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
	public class MakeCommandService : BaseCommandService<Make>, IMakeCommandService
	{

		private readonly IQueryRepository<Make, MakeFilter> queryRepository;
		public MakeCommandService(ICommandRepository<Make> commandRepository,
			ILogger<BaseCommandService<Make>> logger,
			IQueryRepository<Make, MakeFilter> queryRepository) : base(commandRepository, logger)
		{
			this.queryRepository = queryRepository;
		}

		public override string ServiceName => nameof(MakeCommandService);

		public BaseResponse Add(MakeRequest request)
		{
			Make jobTitle = Make.Create(request);

			return Create(jobTitle);
		}

		public override void AfterCreation(Make entity)
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
					Message = "Invalid district selected"
				};
			}


			district.UpdatedAt = DateTime.Now;
			district.UpdatedBy = request.UserId;
			district.Status = Enums.EntityStatus.Deleted;
			var response = Delete(district);

			return response;
		}

		public BaseResponse Edit(MakeRequest request)
		{
			var jobTitle = queryRepository.GetById(request.Id.Value);

			if (jobTitle == null)
				return new BaseResponse
				{
					CodeStatus = Enums.ResponseStatus.Fail,
					Message = "Invalid district selected"
				};
			jobTitle.Name = request.Name;
			jobTitle.UpdatedAt = DateTime.Now;
			jobTitle.UpdatedBy = request.UserId;

			var response = Update(jobTitle);

			return response;
		}

	}
}
