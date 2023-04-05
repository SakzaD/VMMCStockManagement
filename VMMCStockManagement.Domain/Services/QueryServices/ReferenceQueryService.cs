using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Enums;
using VMMCStockManagement.Domain.Interfaces.Repositories;
using VMMCStockManagement.Domain.Interfaces.Services.QueryServices;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse;
using VMMCStockManagement.Domain.Models.Responses;
using VMMCStockManagement.Domain.Entities;

namespace VMMCStockManagement.Domain.Services.QueryServices
{
	public class ReferenceQueryService : BaseQueryService<ReferenceFilter, Reference>, IReferenceQueryService
	{

		public ReferenceQueryService(IQueryRepository<Reference, ReferenceFilter> queryRepository,
			ILogger<BaseService> logger) : base(queryRepository, logger)
		{

		}

		public override string ServiceName => nameof(ReferenceQueryService);

		public ObjectListResponse<ReferenceResponse> Filter(ReferenceFilter filter)
		{
			var response = new ObjectListResponse<ReferenceResponse>();
			var data = queryRepository.Filter(filter).ToList();

			var mappedData = new List<ReferenceResponse>();

			foreach (var item in data)
			{
				mappedData.Add(new ReferenceResponse
				{
					Id = item.Id,
					Number = item.Number
				});
			}
			response.Data = mappedData;
			response.CodeStatus = ResponseStatus.Success;

			return response;
		}

		public ObjectResponse<ReferenceResponse> Get(long id)
		{
			throw new NotImplementedException();
		}
	}
}
