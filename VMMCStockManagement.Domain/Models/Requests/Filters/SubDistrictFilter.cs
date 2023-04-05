using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Requests.Filters
{
	public class SubDistrictFilter : BaseFilter
	{
		public long? DistrictId { get; set; }
	}
}
