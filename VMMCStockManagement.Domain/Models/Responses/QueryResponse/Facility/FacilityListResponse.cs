using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Responses.QueryResponse.Facility
{
	public class FacilityListResponse
	{
		public long Id { get; set; }
		public long SubDistrictId { get; set; }
		public string Name { get; set; }
		public long? DistrictId { get; set; }
		public long ProvinceId { get; set; }
		public long CountryId { get; set; }
	}
}
