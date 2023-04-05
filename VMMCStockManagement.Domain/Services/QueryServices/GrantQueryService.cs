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
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.Grant;
using VMMCStockManagement.Domain.Models.Responses;

namespace VMMCStockManagement.Domain.Services.QueryServices
{
	public class GrantQueryService : BaseQueryService<GrantFilter, Grant>, IGrantQueryService
	{

		public GrantQueryService(IQueryRepository<Grant, GrantFilter> queryRepository,
			ILogger<BaseService> logger) : base(queryRepository, logger)
		{

		}

		public override string ServiceName => nameof(GrantQueryService);

		public ObjectListResponse<GrantListResponse> Filter(GrantFilter filter)
		{
			var response = new ObjectListResponse<GrantListResponse>();
			var data = queryRepository.Filter(filter).ToList();

			var mappedData = new List<GrantListResponse>();

			foreach (var item in data)
			{
				mappedData.Add(new GrantListResponse
				{
					Id = item.Id,
					Name = item.Name,
					CountryName = item.Country?.Name,
					CountryId = item.CountryId
				});
			}
			response.Data = mappedData;
			response.CodeStatus = ResponseStatus.Success;

			return response;
		}

		public ObjectResponse<GrantResponse> Get(long id)
		{
			throw new NotImplementedException();
		}
	}
}
