using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Interfaces.Repositories;
using VMMCStockManagement.Domain.Interfaces.Services.CommandServices;
using VMMCStockManagement.Domain.Interfaces.Services;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Requests;
using VMMCStockManagement.Domain.Models.Responses;
using VMMCStockManagement.Domain.Entities;

namespace VMMCStockManagement.Domain.Services.CommandServices
{
	public class ReferenceCommandService : BaseCommandService<Reference>, IReferenceCommandService
	{

		private readonly IQueryRepository<Reference, ReferenceFilter> queryRepository;
		public ReferenceCommandService(ICommandRepository<Reference> commandRepository,
			ILogger<BaseCommandService<Reference>> logger,
			IQueryRepository<Reference, ReferenceFilter> queryRepository) : base(commandRepository, logger)
		{
			this.queryRepository = queryRepository;
		}

		public override string ServiceName => nameof(ReferenceCommandService);

		public BaseResponse GenerateReference(ReferenceRequest request)
		{
			long number = 100000;

			var ticket = queryRepository.GetAll().OrderBy(x => x.Id).LastOrDefault();

			if (ticket != null)
			{
				number = Convert.ToInt64(ticket.Number) + 1;
			}

			request.Number = number.ToString();

			ticket = Reference.Create(request);
			return Create(ticket);
		}

		public BaseResponse Add(ReferenceRequest request)
		{
			Reference reference = Reference.Create(request);

			return Create(reference);
		}

		public override void AfterCreation(Reference entity)
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

		public BaseResponse Edit(ReferenceRequest request)
		{
			var district = queryRepository.GetById(request.Id.Value);

			if (district == null)
				return new BaseResponse
				{
					CodeStatus = Enums.ResponseStatus.Fail,
					Message = "Invalid asset selected"
				};


			district.UpdatedAt = DateTime.Now;

			var response = Update(district);

			return response;
		}


	}
}
