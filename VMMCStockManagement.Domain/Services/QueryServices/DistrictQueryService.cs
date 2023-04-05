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
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.District;
using VMMCStockManagement.Domain.Models.Responses;
using VMMCStockManagement.Domain.Mappers;

namespace VMMCStockManagement.Domain.Services.QueryServices
{
	public class DistrictQueryService : BaseQueryService<DistrictFilter, District>, IDistrictQueryService
	{

		public DistrictQueryService(IQueryRepository<District, DistrictFilter> queryRepository,
			ILogger<BaseService> logger) : base(queryRepository, logger)
		{

		}

		public override string ServiceName => nameof(DistrictQueryService);

		public ObjectListResponse<DistrictListResponse> Filter(DistrictFilter filter)
		{
			var response = new ObjectListResponse<DistrictListResponse>();
			var data = queryRepository.Filter(filter).ToList();

			var mappedData = data.Map();
			response.Data = mappedData;
			response.CodeStatus = ResponseStatus.Success;

			return response;
		}

		public ObjectResponse<DistrictResponse> Get(long id)
		{
			throw new NotImplementedException();
		}
	}
}
