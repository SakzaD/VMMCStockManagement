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
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.JobTitle;
using VMMCStockManagement.Domain.Models.Responses;

namespace VMMCStockManagement.Domain.Services.QueryServices
{
	public class JobTitleQueryService : BaseQueryService<JobTitleFilter, JobTitle>, IJobTitleQueryService
	{

		public JobTitleQueryService(IQueryRepository<JobTitle, JobTitleFilter> queryRepository,
			ILogger<BaseService> logger) : base(queryRepository, logger)
		{

		}

		public override string ServiceName => nameof(JobTitleQueryService);

		public ObjectListResponse<JobTitleListResponse> Filter(JobTitleFilter filter)
		{
			var response = new ObjectListResponse<JobTitleListResponse>();
			var data = queryRepository.Filter(filter).ToList();

			var mappedData = new List<JobTitleListResponse>();

			foreach (var item in data)
			{
				mappedData.Add(new JobTitleListResponse
				{
					Id = item.Id,
					Name = item.Name
				});
			}
			response.Data = mappedData;
			response.CodeStatus = ResponseStatus.Success;

			return response;
		}

		public ObjectResponse<JobTitleResponse> Get(long id)
		{
			throw new NotImplementedException();
		}
	}
}
