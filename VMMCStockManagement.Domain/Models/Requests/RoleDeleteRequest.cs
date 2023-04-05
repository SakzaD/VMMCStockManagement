using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Requests
{
	public class RoleDeleteRequest
	{
		public string RoleId { get; set; }
		public string UserId { get; set; }
	}
}
