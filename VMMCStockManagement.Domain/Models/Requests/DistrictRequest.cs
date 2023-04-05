using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Requests
{
	public class DistrictRequest : BaseRequest
	{
		public string Name { get; set; }
		public long ProvinceId { get; set; }
	}
}
