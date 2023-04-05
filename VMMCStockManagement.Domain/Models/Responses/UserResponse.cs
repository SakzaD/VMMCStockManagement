using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Responses
{
	public class UserResponse
	{
		public string Id { get; set; }
		public string Username { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public string Token { get; set; }
		public string EmployeeNumber { get; set; }
		public string RoleId { get; set; }
		public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }
		public string Role { get; set; }
	}
}
