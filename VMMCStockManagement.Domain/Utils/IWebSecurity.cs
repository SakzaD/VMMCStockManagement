using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;

namespace VMMCStockManagement.Domain.Utils
{
	public interface IWebSecurity
	{
		public string UserId { get; }
		public string Role { get; }
		public bool HasRole(string role);
		public bool InRole(string role);
		public User User { get; }
		public string DomainUrl { get; }
	}
}
