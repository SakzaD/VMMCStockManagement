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
	public class HardwareTypeCommandService : BaseCommandService<HardwareType>, IHardwareTypeCommandService
	{

		private readonly IQueryRepository<HardwareType, HardwareTypeFilter> queryRepository;
		public HardwareTypeCommandService(ICommandRepository<HardwareType> commandRepository,
			ILogger<BaseCommandService<HardwareType>> logger,
			IQueryRepository<HardwareType, HardwareTypeFilter> queryRepository) : base(commandRepository, logger)
		{
			this.queryRepository = queryRepository;
		}

		public override string ServiceName => nameof(HardwareTypeCommandService);

		public BaseResponse Add(HardwareTypeRequest request)
		{
			HardwareType location = HardwareType.Create(request);

			return Create(location);
		}

		public override void AfterCreation(HardwareType entity)
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

		public BaseResponse Edit(HardwareTypeRequest request)
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
