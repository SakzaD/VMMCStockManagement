using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Models;

namespace VMMCStockManagement.Domain.Interfaces
{
	public interface ITrackingService
	{
		StockTracker? GetRequestStatus(long stockRequestId);
	}
}
