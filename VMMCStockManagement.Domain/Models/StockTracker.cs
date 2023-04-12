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
		public DistrictCoordinatorModel DistrictCoordinator { get; set; } = new DistrictCoordinatorModel();
		public ProgramAdministratorModel ProgramAdministrator { get; set; } = new ProgramAdministratorModel();
		public HOApproverModel HOApprover { get; set; } = new HOApproverModel();

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

	public class HOApproverModel
	{
		public string DateArrived { get; set; }
		public string DateApproved { get; set; } = "Pending";
		public bool IsCompleted { get; set; }
	}

	public class DistrictCoordinatorModel
	{
		public string DateArrived { get; set; }
		public string DateApproved { get; set; } = "Pending";
		public bool IsCompleted { get; set; }
	}

	public class ProgramAdministratorModel
	{
		public string DateArrived { get; set; }
		public string DateApproved { get; set; } = "Pending";
		public bool IsCompleted { get; set; }
	}
}
