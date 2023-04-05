using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Responses
{
	public class ObjectListResponse<T> : BaseResponse where T : class
	{
		public ObjectListResponse() 
		{ 
			Data = new List<T>();
		}

		public List<T> Data { get; set; }
	}
}
