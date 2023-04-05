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
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.Province;
using VMMCStockManagement.Domain.Models.Responses;
using VMMCStockManagement.Domain.Mappers;

namespace VMMCStockManagement.Domain.Services.QueryServices
{
	public class ProvinceQueryService : BaseQueryService<ProvinceFilter, Province>, IProvinceQueryService
	{

		public ProvinceQueryService(IQueryRepository<Province, ProvinceFilter> queryRepository,
			ILogger<BaseService> logger) : base(queryRepository, logger)
		{

		}

		public override string ServiceName => nameof(ProvinceQueryService);

		public ObjectListResponse<ProvinceListResponse> Filter(ProvinceFilter filter)
		{
			var response = new ObjectListResponse<ProvinceListResponse>();
			var data = queryRepository.Filter(filter).ToList();

			var mappedData = data.Map();
			response.Data = mappedData;
			response.CodeStatus = ResponseStatus.Success;

			return response;
		}

		public ObjectResponse<ProvinceResponse> Get(long id)
		{
			throw new NotImplementedException();
		}
	}
}
