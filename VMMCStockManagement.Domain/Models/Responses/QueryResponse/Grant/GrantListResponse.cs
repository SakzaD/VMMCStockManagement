using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Responses.QueryResponse.Grant
{
	public class GrantListResponse
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public long? CountryId { get; set; }
		public string CountryName { get; set; }
	}
}
