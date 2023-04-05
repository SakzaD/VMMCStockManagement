using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Requests.Filters
{
	public class StockFilter : BaseFilter
	{
		public string? DeviceIdentifier { get; set; }
		public string? SearchValue { get; set; }
	}
}
