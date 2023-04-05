using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Responses.QueryResponse.Province
{
	public class ProvinceListResponse
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public long CountryId { get; set; }
	}
}
