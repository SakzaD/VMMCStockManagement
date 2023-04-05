using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Requests.Filters
{
	public class StockRequestReportFilter : BaseFilter
	{
		public long? DepartmentId { get; set; }
		public long? JobTitleId { get; set; }
		public long? GrantId { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
	}
}
