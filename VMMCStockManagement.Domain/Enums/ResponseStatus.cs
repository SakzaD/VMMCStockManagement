using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Enums
{
	public enum ResponseStatus
	{
		Success = 1,
		Failed,
		Unauthorized,
		UserExist,
		NoneExist,
		Fail
	}
}
