using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models
{
	public class UserModel
	{
		public int RowNumber { get; set; }
		public string Grant { get; set; }
		public string Province { get; set; }
		public string EmployeeCode { get; set; }
		public string KnownAsName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string JobTitle { get; set; }
		public string ContactNumber { get; set; }
		public string Email { get; set; }
	}
}
