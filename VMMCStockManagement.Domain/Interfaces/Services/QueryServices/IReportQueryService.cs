using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse;
using VMMCStockManagement.Domain.Models.Responses;

namespace VMMCStockManagement.Domain.Interfaces.Services.QueryServices
{
	public interface IReportQueryService
	{
		ObjectListResponse<ReportListResponse> GenerateAssetReport(ReportFilter reportFilter);
	}
}
