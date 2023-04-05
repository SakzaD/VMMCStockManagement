using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Enums;

namespace VMMCStockManagement.Domain.Models.Responses
{
	public class BaseResponse
	{
		public ResponseStatus CodeStatus {  get; set; }
		public string Status { get; set; }
		public string Message { get; set; }

	}
}
