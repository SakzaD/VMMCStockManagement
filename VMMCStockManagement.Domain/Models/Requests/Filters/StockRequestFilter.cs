using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Enums;

namespace VMMCStockManagement.Domain.Models.Requests.Filters
{
	public class StockRequestFilter : BaseFilter
	{
		public string? Type { get; set; }
		public bool IsInitiator { get; set; } = false;
		public bool IsLineManager { get; set; } = false;
		public AccessRole AccessRole { get; set; } = AccessRole.Requester;
	}
}
