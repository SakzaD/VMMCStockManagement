using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Requests
{
	public class ReasonRequest : BaseRequest
	{
		public string Description { get; set; }
		public long ReasonCategoryId { get; set; }
	}
}
