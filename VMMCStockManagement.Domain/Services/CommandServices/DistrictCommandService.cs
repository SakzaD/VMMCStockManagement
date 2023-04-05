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
	public class DistrictCommandService : BaseCommandService<District>, IDistrictCommandService
	{
		private readonly IQueryRepository<District, DistrictFilter> queryRepository;
		public DistrictCommandService(ICommandRepository<District> commandRepository,
			ILogger<BaseCommandService<District>> logger,
			IQueryRepository<District, DistrictFilter> queryRepository) : base(commandRepository, logger)
		{
			this.queryRepository = queryRepository;
		}

		public override string ServiceName => nameof(DistrictCommandService);

		public BaseResponse Add(DistrictRequest request)
		{
			District deliveryModel = District.Create(request.Name, request.ProvinceId, request.UserId);

			return Create(deliveryModel);
		}

		public override void AfterCreation(District entity)
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
			var response = Delete(district);

			return response;
		}

		public BaseResponse Edit(DistrictRequest request)
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
			district.Name = request.Name;
			var response = Update(district);

			return response;
		}
	}
}
