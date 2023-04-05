using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Interfaces.Repositories;
using VMMCStockManagement.Domain.Interfaces.Services;
using VMMCStockManagement.Domain.Interfaces.Services.CommandServices;
using VMMCStockManagement.Domain.Models.Requests;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Responses;

namespace VMMCStockManagement.Domain.Services.CommandServices
{
	public class StaffManagerCommandService : BaseCommandService<StaffManager>, IStaffManagerCommandService
	{

		private readonly IQueryRepository<StaffManager, StaffManagerFilter> queryRepository;
		public StaffManagerCommandService(ICommandRepository<StaffManager> commandRepository,
			ILogger<BaseCommandService<StaffManager>> logger,
			IQueryRepository<StaffManager, StaffManagerFilter> queryRepository) : base(commandRepository, logger)
		{
			this.queryRepository = queryRepository;
		}

		public override string ServiceName => nameof(StaffManagerCommandService);

		public BaseResponse Add(StaffManagerRequest request)
		{
			StaffManager staffManager = StaffManager.Create(request);

			return Create(staffManager);
		}

		public override void AfterCreation(StaffManager entity)
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
					Message = "Invalid asset selected"
				};
			}


			district.UpdatedAt = DateTime.Now;
			district.UpdatedBy = request.UserId;
			district.Status = Enums.EntityStatus.Deleted;
			var response = Delete(district);

			return response;
		}

		public BaseResponse Edit(StaffManagerRequest request)
		{
			var district = queryRepository.GetById(request.Id.Value);

			if (district == null)
				return new BaseResponse
				{
					CodeStatus = Enums.ResponseStatus.Fail,
					Message = "Invalid asset selected"
				};


			district.UpdatedAt = DateTime.Now;
			district.ManagerId = request.ManagerId;

			var response = Update(district);

			return response;
		}
	}
}
