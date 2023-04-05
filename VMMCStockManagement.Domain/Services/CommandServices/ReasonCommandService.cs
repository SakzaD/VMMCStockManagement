using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Interfaces.Repositories;
using VMMCStockManagement.Domain.Interfaces.Services.CommandServices;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Requests;
using VMMCStockManagement.Domain.Models.Responses;

namespace VMMCStockManagement.Domain.Services.CommandServices
{
	public class ReasonCommandService : BaseCommandService<Reason>, IReasonCommandService
	{

		private readonly IQueryRepository<Reason, ReasonFilter> queryRepository;
		public ReasonCommandService(ICommandRepository<Reason> commandRepository,
			ILogger<BaseCommandService<Reason>> logger,
			IQueryRepository<Reason, ReasonFilter> queryRepository) : base(commandRepository, logger)
		{
			this.queryRepository = queryRepository;
		}

		public override string ServiceName => nameof(ReasonCommandService);

		public BaseResponse Add(ReasonRequest request)
		{
			Reason reason = Reason.Create(request);

			return Create(reason);
		}

		public override void AfterCreation(Reason entity)
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
					Message = "Invalid reason selected"
				};
			}


			district.UpdatedAt = DateTime.Now;
			district.UpdatedBy = request.UserId;
			district.Status = Enums.EntityStatus.Deleted;
			var response = Delete(district);

			return response;
		}

		public BaseResponse Edit(ReasonRequest request)
		{
			var district = queryRepository.GetById(request.Id.Value);

			if (district == null)
				return new BaseResponse
				{
					CodeStatus = Enums.ResponseStatus.Fail,
					Message = "Invalid reason selected"
				};


			district.UpdatedAt = DateTime.Now;
			district.Description = request.Description;

			var response = Update(district);

			return response;
		}
	}
}
