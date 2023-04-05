using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Responses
{
	public class CreationResponse<T> : BaseResponse where T : class
	{
		public T? CreatedEntity { get; set; }
	}
}
