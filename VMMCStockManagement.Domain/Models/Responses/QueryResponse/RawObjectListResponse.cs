using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Responses.QueryResponse
{
	public class RawObjectListResponse<T> : BaseResponse where T : class
	{
		public IQueryable<T>? Data { get; set; }
	}
}
