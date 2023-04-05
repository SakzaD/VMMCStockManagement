using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models
{
	public class StockTracker
	{
		public string? ReferenceNumber { get; set; }
		public string TicketNumber { get; set; }
		public RequesterModel Requester { get; set; } = new RequesterModel();
		public DataCapureModel DataCapure { get; set; } = new DataCapureModel();
		public SupplierModel Supplier { get; set; } = new SupplierModel();		
		public List<RequestedItemModel> RequestedItems { get; set; } = new List<RequestedItemModel>();
	}

	public class RequestedItemModel
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public int Qty { get; set; }
	}

	public class RequesterModel
	{
		public string DateRequested { get; set; }
		public string FullName { get; set; }
		public string EmployeeNumber { get; set; }
		public string Email { get; set; }
	}

	public class SupplierModel
	{
		public string DateArrived { get; set; }
		public string DateApproved { get; set; } = "Pending";
		public bool IsCompleted { get; set; }
	}

	public class DataCapureModel
	{
		public string DateArrived { get; set; }
		public string DateApproved { get; set; } = "Pending";
		public bool IsCompleted { get; set; }
	}
}
