using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Enums;

namespace VMMCStockManagement.Domain.Models.Requests.Filters
{
	public class RequestApprovalFilter : BaseFilter
	{
		public StockStatus StockStatus { get; set; }
	}
}
