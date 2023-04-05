using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Responses.QueryResponse.Reason
{
	public class ReasonResponse
	{
		public long Id { get; set; }
		public string Description { get; set; }
		public string Category { get; set; }
		public string Type { get; set; }
	}
}
