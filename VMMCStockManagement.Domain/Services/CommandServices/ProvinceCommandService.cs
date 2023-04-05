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
	public class ProvinceCommandService : BaseCommandService<Province>, IProvinceCommandService
	{
		private readonly IQueryRepository<Province, ProvinceFilter> queryRepository;
		public ProvinceCommandService(ICommandRepository<Province> commandRepository,
			ILogger<BaseCommandService<Province>> logger,
			IQueryRepository<Province, ProvinceFilter> queryRepository) : base(commandRepository, logger)
		{
			this.queryRepository = queryRepository;
		}

		public override string ServiceName => nameof(ProvinceCommandService);

		public BaseResponse Add(ProvinceRequest request)
		{
			Province entiry = Province
				.Create(request.Name, request.CountryId, request.UserId);

			return Create(entiry);
		}

		public override void AfterCreation(Province entity)
		{

		}

		public BaseResponse Delete(BaseRequest request)
		{
			var province = queryRepository.GetById(request.Id.Value);

			if (province == null)
			{
				return new BaseResponse
				{
					CodeStatus = Enums.ResponseStatus.Fail,
					Message = "Invalid province selected"
				};
			}

			province.UpdatedAt = DateTime.Now;
			province.UpdatedBy = request.UserId;
			var response = Delete(province);

			return response;
		}

		public BaseResponse Edit(ProvinceRequest request)
		{
			var province = queryRepository.GetById(request.Id.Value);

			if (province == null)
				return new BaseResponse
				{
					CodeStatus = Enums.ResponseStatus.Fail,
					Message = "Invalid province selected"
				};

			province.UpdatedAt = DateTime.Now;
			province.UpdatedBy = request.UserId;
			province.Name = request.Name;
			var response = Update(province);

			return response;
		}
	}
}
