using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Responses.QueryResponse.StaffManager
{
	public class StaffManagerResponse
	{
		public long? Id { get; set; }
		public string StaffName { get; set; }
		public string ManagerName { get; set; }
	}
}
