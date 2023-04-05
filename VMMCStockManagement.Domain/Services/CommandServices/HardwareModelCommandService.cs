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
	public class HardwareModelCommandService : BaseCommandService<HardwareModel>, IHardwareModelCommandService
	{

		private readonly IQueryRepository<HardwareModel, HardwareModelFilter> queryRepository;
		public HardwareModelCommandService(ICommandRepository<HardwareModel> commandRepository,
			ILogger<BaseCommandService<HardwareModel>> logger,
			IQueryRepository<HardwareModel, HardwareModelFilter> queryRepository) : base(commandRepository, logger)
		{
			this.queryRepository = queryRepository;
		}

		public override string ServiceName => nameof(HardwareModelCommandService);

		public BaseResponse Add(HardwareModelRequest request)
		{
			HardwareModel location = HardwareModel.Create(request);

			return Create(location);
		}

		public override void AfterCreation(HardwareModel entity)
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

		public BaseResponse Edit(HardwareModelRequest request)
		{
			var district = queryRepository.GetById(request.Id.Value);

			if (district == null)
				return new BaseResponse
				{
					CodeStatus = Enums.ResponseStatus.Fail,
					Message = "Invalid district selected"
				};

			district.UpdatedAt = DateTime.Now;
			district.UpdatedBy = request.UserId;

			var response = Update(district);

			return response;
		}
	}
}
