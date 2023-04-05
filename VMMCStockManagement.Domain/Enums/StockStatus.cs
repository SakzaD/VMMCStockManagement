using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Enums
{
	public enum StockStatus
	{
		Pending = 1,
		Approved = 2,
		Rejected = 3,
		Completed = 4,
		Unknown = 5,
		Amend = 6,

	}
}
