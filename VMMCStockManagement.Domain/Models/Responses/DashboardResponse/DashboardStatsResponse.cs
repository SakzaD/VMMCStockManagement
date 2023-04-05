using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Responses.DashboardResponse
{
	public class DashboardStatsResponse
	{
		public int TotalRequest { get; set; }
		public int TotalReturn { get; set; }
		public int TotalTransfer { get; set; }
		public int TotalRepair { get; set; }
		public int TotalDamageOrLoss { get; set; }
		public int TotalDisposal { get; set; }
		public int TotalReplacement { get; set; }
		public int TotalAllAssets { get; set; }
		public int TotalMyAssets { get; set; }

		public List<DailyReportResponse> DailyStats { get; set; } = new List<DailyReportResponse>();
		public List<MonthlyReportResponse> MonthlyStats { get; set; } = new List<MonthlyReportResponse>();
	}
}
