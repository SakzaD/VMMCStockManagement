
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Enums;

namespace VMMCStockManagement.Domain.Entities
{
	public partial class User : IdentityUser
	{
        public string FirstName { get; set; }
		public string LastName { get; set; }
		public string? EmployeeNumber { get; set; }
		public string? IdNumber { get; set;}
		public string? KnownAsName { get; set;}
		public long? JobTitleId { get; set; }
		public long? GrantId { get; set; }
		public long? LoacationId { get; set; }
		public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }
		public virtual ICollection<UserRole> UserRoles { get; set;}
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public string CreatedBy { get; set; }
		public string UpdatedBy { get; set; }
		public EntityStatus Status { get; set; } = EntityStatus.Active;

	}
}
