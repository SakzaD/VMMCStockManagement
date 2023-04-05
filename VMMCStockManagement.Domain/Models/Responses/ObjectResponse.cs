using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Responses
{
	public class ObjectResponse<T> : BaseResponse where T : class
	{
		public T? Data { get; set; }
	}
}
