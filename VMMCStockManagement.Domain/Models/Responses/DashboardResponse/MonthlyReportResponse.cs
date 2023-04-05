using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Responses.DashboardResponse
{
	public class MonthlyReportResponse
	{
		public string Month { get; set; }
		public string Total { get; set; }
	}
}
