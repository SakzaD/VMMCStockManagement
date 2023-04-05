using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Responses.QueryResponse.User
{
	public class UserSearchResponse
	{
		public string EmployeeId { get; set; }
		public string EmployeeName { get; set; }
		public string Email { get; set; }
	}
}
