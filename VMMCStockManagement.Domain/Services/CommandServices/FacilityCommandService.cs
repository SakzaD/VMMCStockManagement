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
	public class FacilityCommandService : BaseCommandService<Facility>, IFacilityCommandService
	{
		private readonly IQueryRepository<Facility, FacilityFilter> queryRepository;
		public FacilityCommandService(ICommandRepository<Facility> commandRepository,
			ILogger<BaseCommandService<Facility>> logger,
			IQueryRepository<Facility, FacilityFilter> queryRepository) : base(commandRepository, logger)
		{
			this.queryRepository = queryRepository;
		}

		public override string ServiceName => nameof(FacilityCommandService);

		public BaseResponse Add(FacilityRequest request)
		{
			Facility entity = Facility.Create(request);

			return Create(entity);
		}

		public override void AfterCreation(Facility entity)
		{

		}

		public BaseResponse Delete(BaseRequest request)
		{
			var facility = queryRepository.GetById(request.Id.Value);

			if (facility == null)
			{
				return new BaseResponse
				{
					CodeStatus = Enums.ResponseStatus.Fail,
					Message = "Invalid facility selected"
				};
			}

			facility.UpdatedAt = DateTime.Now;
			facility.UpdatedBy = request.UserId;
			var response = Delete(facility);

			return response;
		}

		public BaseResponse Edit(FacilityRequest request)
		{
			var facility = queryRepository.GetById(request.Id.Value);

			if (facility == null)
				return new BaseResponse
				{
					CodeStatus = Enums.ResponseStatus.Fail,
					Message = "Invalid facility selected"
				};

			facility.UpdatedAt = DateTime.Now;
			facility.UpdatedBy = request.UserId;
			facility.Name = request.Name;
			var response = Update(facility);

			return response;
		}
	}
}
