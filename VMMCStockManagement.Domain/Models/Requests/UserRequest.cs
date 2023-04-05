using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Requests
{
	public class UserRequest : BaseRequest
	{
		public string? Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string? EmployeeNumber { get; set; }
		public string? PhoneNumber { get; set; }
		public long? FacilityId { get; set; }
		public List<string>? Facility { get; set; }
		public List<string>? Roles { get; set; }
		public string RoleId { get; set; }
		public string? Password { get; set; }
		public string? IdNumber { get; set; }
		public string? KnownAsName { get; set; }
		public long? GrantId { get; set; }
		public long? ProvinceId { get; set; }
		public long? JobTitleId { get; set; }

	}
}
