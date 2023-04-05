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
using VMMCStockManagement.Domain.Models.Responses;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.StaffManager;

namespace VMMCStockManagement.Domain.Services.QueryServices
{
	public class StaffManagerQueryService : BaseQueryService<StaffManagerFilter, StaffManager>, IStaffManagerQueryService
	{
		public StaffManagerQueryService(IQueryRepository<StaffManager, StaffManagerFilter> queryRepository,
			ILogger<BaseService> logger) : base(queryRepository, logger)
		{
		}

		public override string ServiceName => nameof(StaffManagerQueryService);
		public ObjectListResponse<StaffManagerListResponse> Filter(StaffManagerFilter filter)
		{
			var response = new ObjectListResponse<StaffManagerListResponse>();
			var data = queryRepository.Filter(filter).ToList();

			var mappedData = new List<StaffManagerListResponse>();

			foreach (var item in data)
			{
				mappedData.Add(new StaffManagerListResponse
				{
					Id = item.Id,



				});
			}
			response.Data = mappedData;
			response.CodeStatus = ResponseStatus.Success;

			return response;
		}
		public ObjectResponse<StaffManagerResponse> Get(long id)
		{
			var response = new ObjectResponse<StaffManagerResponse>();

			var item = queryRepository.GetById(id);

			var mappedData = new StaffManagerResponse
			{
				Id = item.Id,

			};

			response.Data = mappedData;
			response.CodeStatus = ResponseStatus.Success;

			return response;
		}
	}
}
