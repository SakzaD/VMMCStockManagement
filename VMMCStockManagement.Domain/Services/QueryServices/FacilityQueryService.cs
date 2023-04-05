using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Enums;
using VMMCStockManagement.Domain.Interfaces.Repositories;
using VMMCStockManagement.Domain.Interfaces.Services.QueryServices;
using VMMCStockManagement.Domain.Mappers;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Responses;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.Facility;

namespace VMMCStockManagement.Domain.Services.QueryServices
{
	public class FacilityQueryService : BaseQueryService<FacilityFilter, Facility>, IFacilityQueryService
	{

		public FacilityQueryService(IQueryRepository<Facility, FacilityFilter> queryRepository,
			ILogger<BaseService> logger) : base(queryRepository, logger)
		{

		}

		public override string ServiceName => nameof(FacilityQueryService);

		public ObjectListResponse<FacilityListResponse> Filter(FacilityFilter filter)
		{
			var response = new ObjectListResponse<FacilityListResponse>();
			var data = queryRepository.Filter(filter).ToList();

			var mappedData = data.Map();
			response.Data = mappedData;
			response.CodeStatus = ResponseStatus.Success;

			return response;
		}

		public ObjectResponse<FacilityResponse> Get(long id)
		{
			throw new NotImplementedException();
		}
	}
}
