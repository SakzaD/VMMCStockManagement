using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Enums
{
	public enum AccessRole
	{
		DataCapture = 1,
		SiteManager,
		DistrictManager,
		StockManager,
		Supplier,
		Admin,
		Unknown
	}
}
