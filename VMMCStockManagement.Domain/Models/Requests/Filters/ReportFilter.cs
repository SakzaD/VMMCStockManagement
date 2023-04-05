using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Requests.Filters
{
	public class ReportFilter : BaseFilter
	{
		public long? DepartmentId { get; set; }
		public long? GrantId { get; set; }
		public long? LocationId { get; set; }
		public long? AssetCategoryId { get; set; }
		public string? UserAssignedId { get; set; }
		public string? RegistrationNumber { get; set; }
		public string? SerialNumber { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
	}
}
