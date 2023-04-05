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
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.SubDistrict;
using VMMCStockManagement.Domain.Models.Responses;
using VMMCStockManagement.Domain.Mappers;

namespace VMMCStockManagement.Domain.Services.QueryServices
{
	public class SubDistrictQueryService : BaseQueryService<SubDistrictFilter, SubDistrict>, ISubDistrictQueryService
	{

		public SubDistrictQueryService(IQueryRepository<SubDistrict, SubDistrictFilter> queryRepository,
			ILogger<BaseService> logger) : base(queryRepository, logger)
		{

		}

		public override string ServiceName => nameof(SubDistrictQueryService);

		public ObjectListResponse<SubDistrictListResponse> Filter(SubDistrictFilter filter)
		{
			var response = new ObjectListResponse<SubDistrictListResponse>();
			var data = queryRepository.Filter(filter).ToList();

			var mappedData = data.Map();
			response.Data = mappedData;
			response.CodeStatus = ResponseStatus.Success;

			return response;
		}

		public ObjectResponse<SubDistrictResponse> Get(long id)
		{
			throw new NotImplementedException();
		}
	}
}
