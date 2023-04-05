using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Enums;

namespace VMMCStockManagement.Domain.Models.Requests
{
	public class StockTrackRequest : BaseRequest
	{
        public long? StockId { get; set; }
		public Category Category { get; set; }
		public StockStatus? StockStatus { get; set; }

	}
}
