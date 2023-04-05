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
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.Location;
using VMMCStockManagement.Domain.Models.Responses;

namespace VMMCStockManagement.Domain.Services.QueryServices
{
	public class LocationQueryService : BaseQueryService<LocationFilter, Location>, ILocationQueryService
	{

		public LocationQueryService(IQueryRepository<Location, LocationFilter> queryRepository,
			ILogger<BaseService> logger) : base(queryRepository, logger)
		{

		}

		public override string ServiceName => nameof(LocationQueryService);

		public ObjectListResponse<LocationListResponse> Filter(LocationFilter filter)
		{
			var response = new ObjectListResponse<LocationListResponse>();
			var data = queryRepository.Filter(filter).ToList();

			var mappedData = new List<LocationListResponse>();

			foreach (var item in data)
			{
				mappedData.Add(new LocationListResponse
				{
					Id = item.Id,
					Name = item.Name
				});
			}
			response.Data = mappedData;
			response.CodeStatus = ResponseStatus.Success;

			return response;
		}

		public ObjectResponse<LocationResponse> Get(long id)
		{
			throw new NotImplementedException();
		}
	}
}
