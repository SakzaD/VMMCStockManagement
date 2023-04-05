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
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.Make;
using VMMCStockManagement.Domain.Models.Responses;

namespace VMMCStockManagement.Domain.Services.QueryServices
{
	public class MakeQueryService : BaseQueryService<MakeFilter, Make>, IMakeQueryService
	{

		public MakeQueryService(IQueryRepository<Make, MakeFilter> queryRepository,
			ILogger<BaseService> logger) : base(queryRepository, logger)
		{

		}

		public override string ServiceName => nameof(MakeQueryService);

		public ObjectListResponse<MakeListResponse> Filter(MakeFilter filter)
		{
			var response = new ObjectListResponse<MakeListResponse>();
			var data = queryRepository.Filter(filter).ToList();

			var mappedData = new List<MakeListResponse>();

			foreach (var item in data)
			{
				mappedData.Add(new MakeListResponse
				{
					Id = item.Id,
					Name = item.Name
				});
			}
			response.Data = mappedData;
			response.CodeStatus = ResponseStatus.Success;

			return response;
		}

		public ObjectResponse<MakeResponse> Get(long id)
		{
			throw new NotImplementedException();
		}
	}
}
