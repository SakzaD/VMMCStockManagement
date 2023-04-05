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
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.Department;
using VMMCStockManagement.Domain.Models.Responses;

namespace VMMCStockManagement.Domain.Services.QueryServices
{
	public class DepartmentQueryService : BaseQueryService<DepartmentFilter, Department>, IDepartmentQueryService
	{

		public DepartmentQueryService(IQueryRepository<Department, DepartmentFilter> queryRepository,
			ILogger<BaseService> logger) : base(queryRepository, logger)
		{

		}

		public override string ServiceName => nameof(DepartmentQueryService);

		public ObjectListResponse<DepartmentListResponse> Filter(DepartmentFilter filter)
		{
			var response = new ObjectListResponse<DepartmentListResponse>();
			var data = queryRepository.Filter(filter).ToList();

			var mappedData = new List<DepartmentListResponse>();

			foreach (var item in data)
			{
				mappedData.Add(new DepartmentListResponse
				{
					Id = item.Id,
					Name = item.Name
				});
			}
			response.Data = mappedData;
			response.CodeStatus = ResponseStatus.Success;

			return response;
		}

		public ObjectResponse<DepartmentResponse> Get(long id)
		{
			throw new NotImplementedException();
		}
	}
}

