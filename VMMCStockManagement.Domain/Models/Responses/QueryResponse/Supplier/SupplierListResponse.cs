using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Responses.QueryResponse.Supplier
{
	public class SupplierListResponse
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
	}
}
