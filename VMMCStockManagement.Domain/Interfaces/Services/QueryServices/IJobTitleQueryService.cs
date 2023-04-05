using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.JobTitle;
using VMMCStockManagement.Domain.Models.Responses;

namespace VMMCStockManagement.Domain.Interfaces.Services.QueryServices
{
	public interface IJobTitleQueryService : IBaseQueryService<JobTitleFilter, JobTitle>
	{
		ObjectListResponse<JobTitleListResponse> Filter(JobTitleFilter filter);
		ObjectResponse<JobTitleResponse> Get(long id);
	}
}
