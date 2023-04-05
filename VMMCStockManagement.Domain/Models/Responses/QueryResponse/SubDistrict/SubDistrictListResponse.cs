using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Responses.QueryResponse.SubDistrict
{
	public class SubDistrictListResponse
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public long DistrictId { get; set; }
		public long ProvinceId { get; set; }
		public long CountryId { get; set; }
	}
}
