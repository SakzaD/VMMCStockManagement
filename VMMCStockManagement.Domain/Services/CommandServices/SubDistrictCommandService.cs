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
	public class SubDistrictCommandService : BaseCommandService<SubDistrict>, ISubDistrictCommandService
	{
		private readonly IQueryRepository<SubDistrict, SubDistrictFilter> queryRepository;
		public SubDistrictCommandService(ICommandRepository<SubDistrict> commandRepository,
			ILogger<BaseCommandService<SubDistrict>> logger, IQueryRepository<SubDistrict, SubDistrictFilter> queryRepository) : base(commandRepository, logger)
		{
			this.queryRepository = queryRepository;
		}

		public override string ServiceName => nameof(SubDistrictCommandService);

		public BaseResponse Add(SubDistrictRequest request)
		{
			SubDistrict entiry = SubDistrict
				.Create(request.Name, request.DistrictId, request.UserId);

			return Create(entiry);
		}

		public override void AfterCreation(SubDistrict entity)
		{

		}

		public BaseResponse Delete(BaseRequest request)
		{
			var subDistrict = queryRepository.GetById(request.Id.Value);

			if (subDistrict == null)
			{
				return new BaseResponse
				{
					CodeStatus = Enums.ResponseStatus.Fail,
					Message = "Invalid sub-district selected"
				};
			}

			subDistrict.UpdatedAt = DateTime.Now;
			subDistrict.UpdatedBy = request.UserId;
			var response = Delete(subDistrict);

			return response;
		}

		public BaseResponse Edit(SubDistrictRequest request)
		{
			var subDistrict = queryRepository.GetById(request.Id.Value);

			if (subDistrict == null)
				return new BaseResponse
				{
					CodeStatus = Enums.ResponseStatus.Fail,
					Message = "Invalid sub-district selected"
				};

			subDistrict.UpdatedAt = DateTime.Now;
			subDistrict.UpdatedBy = request.UserId;
			subDistrict.Name = request.Name;
			var response = Update(subDistrict);

			return response;
		}
	}
}
