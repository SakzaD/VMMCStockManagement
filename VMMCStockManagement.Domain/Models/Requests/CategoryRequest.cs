using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Requests
{
	public class CategoryRequest : BaseRequest
	{
		public string Name { get; set; }
		public string Description { get; set; }
	}
}
