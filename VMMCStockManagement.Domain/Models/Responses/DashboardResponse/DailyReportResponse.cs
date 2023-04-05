using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Responses.DashboardResponse
{
	public class DailyReportResponse
	{
		public string Day { get; set; }
		public string Total { get; set; }
	}
}
