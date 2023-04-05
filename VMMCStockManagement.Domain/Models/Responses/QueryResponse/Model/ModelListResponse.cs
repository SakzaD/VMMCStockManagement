using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Responses.QueryResponse.Model
{
	public class ModelListResponse
	{
		public long Id { get; set; }
		public long? MakeId { get; set; }
		public string Name { get; set; }
		public string Make { get; set; }
	}
}
