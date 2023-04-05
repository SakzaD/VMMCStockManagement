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
	public class GrantCommandService : BaseCommandService<Grant>, IGrantCommandService
	{

		private readonly IQueryRepository<Grant, GrantFilter> queryRepository;
		public GrantCommandService(ICommandRepository<Grant> commandRepository,
			ILogger<BaseCommandService<Grant>> logger,
			IQueryRepository<Grant, GrantFilter> queryRepository) : base(commandRepository, logger)
		{
			this.queryRepository = queryRepository;
		}

		public override string ServiceName => nameof(GrantCommandService);

		public BaseResponse Add(GrantRequest request)
		{
			Grant grant = Grant.Create(request);

			return Create(grant);
		}

		public override void AfterCreation(Grant entity)
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

		public BaseResponse Edit(GrantRequest request)
		{
			var district = queryRepository.GetById(request.Id.Value);

			if (district == null)
				return new BaseResponse
				{
					CodeStatus = Enums.ResponseStatus.Fail,
					Message = "Invalid district selected"
				};

			district.Name = request.Name;
			district.CountryId = request.CountryId;
			district.UpdatedAt = DateTime.Now;
			district.UpdatedBy = request.UserId;

			var response = Update(district);

			return response;
		}
	}
}
