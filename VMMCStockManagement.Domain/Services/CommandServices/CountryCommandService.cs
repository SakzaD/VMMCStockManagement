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
	public class CountryCommandService : BaseCommandService<Country>, ICountryCommandService
	{
		private readonly IQueryRepository<Country, CountryFilter> queryRepository;
		public CountryCommandService(ICommandRepository<Country> commandRepository,
			ILogger<BaseCommandService<Country>> logger, IQueryRepository<Country, CountryFilter> queryRepository) : base(commandRepository, logger)
		{
			this.queryRepository = queryRepository;
		}

		public override string ServiceName => nameof(CountryCommandService);

		public BaseResponse Add(CountryRequest request)
		{
			Country entiry = Country
				.Create(request);

			return Create(entiry);
		}

		public override void AfterCreation(Country entity)
		{

		}

		public BaseResponse Delete(BaseRequest request)
		{
			var country = queryRepository.GetById(request.Id.Value);

			if (country == null)
				return new BaseResponse
				{
					CodeStatus = Enums.ResponseStatus.Fail,
					Message = "Invalid country selected"
				};

			country.UpdatedAt = DateTime.Now;
			country.UpdatedBy = request.UserId;
			var response = Delete(country);

			return response;
		}

		public BaseResponse Edit(CountryRequest request)
		{
			var country = queryRepository.GetById(request.Id.Value);

			if (country == null)
				return new BaseResponse
				{
					CodeStatus = Enums.ResponseStatus.Fail,
					Message = "Invalid country selected"
				};

			country.UpdatedAt = DateTime.Now;
			country.UpdatedBy = request.UserId;
			country.Name = request.Name;
			var response = Update(country);

			return response;
		}
	}
}

