using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Requests
{
	public class BaseRequest
	{
		public long? Id { get; set; }

		[Required]
		public string UserId { get; set; }
	}
}
