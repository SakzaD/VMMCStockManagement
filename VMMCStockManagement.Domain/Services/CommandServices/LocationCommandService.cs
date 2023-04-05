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
	public class LocationCommandService : BaseCommandService<Location>, ILocationCommandService
	{

		private readonly IQueryRepository<Location, LocationFilter> queryRepository;
		public LocationCommandService(ICommandRepository<Location> commandRepository,
			ILogger<BaseCommandService<Location>> logger,
			IQueryRepository<Location, LocationFilter> queryRepository) : base(commandRepository, logger)
		{
			this.queryRepository = queryRepository;
		}

		public override string ServiceName => nameof(LocationCommandService);

		public BaseResponse Add(LocationRequest request)
		{
			Location location = Location.Create(request);

			return Create(location);
		}

		public override void AfterCreation(Location entity)
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

		public BaseResponse Edit(LocationRequest request)
		{
			var district = queryRepository.GetById(request.Id.Value);

			if (district == null)
				return new BaseResponse
				{
					CodeStatus = Enums.ResponseStatus.Fail,
					Message = "Invalid district selected"
				};

			district.UpdatedAt = DateTime.Now;
			district.Name = request.Name;
			district.UpdatedBy = request.UserId;

			var response = Update(district);

			return response;
		}
	}
}
