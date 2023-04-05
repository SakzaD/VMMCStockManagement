using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Enums;

namespace VMMCStockManagement.Domain.Models.Requests
{
	public class RequestApprovalRequest : BaseRequest
	{
		public StockStatus StockStatus { get; set; }
	}
}
