using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Requests.Filters
{
	public class BaseFilter
	{
		public long Id { get; set; }
		public string? UserId { get; set; }
		public int Page { get; set; }
	}
}
