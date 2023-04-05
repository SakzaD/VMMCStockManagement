using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Enums;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.StockRequest;

namespace VMMCStockManagement.Domain.Models.Responses.QueryResponse.RequestStock
{
	public class StockRequestResponse
	{
		public long Id { get; set; }
		public bool CanAction { get; set; }
		public AccessRole AccessRole { get; set; }
		public string StatusDescription { get; set; }
		public string Status { get; set; }
		public string EmployeeNumber { get; set; }
		public string ReferenceNumber { get; set; }
		public string TicketNumber { get; set; }
		public string DateCreated { get; set; }

		public string FullName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }

		public string Department { get; set; }
		public long DepartmentId { get; set; }
		public string DateRequired { get; set; }
		public string JobTitle { get; set; }
		public long JobTitleId { get; set; }
		public string Facility { get; set; }
		public long FacilityId { get; set; }
		public string Manager { get; set; }
		public string ManagerId { get; set; }
		public string Reason { get; set; }
		public long ReasonId { get; set; }
		public string Phone { get; set; }
		public string Grant { get; set; }
		public long GrantId { get; set; }

		public string Country { get; set; }
		public long CountryId { get; set; }

		public List<RequestedStockResponse> RequestedStockResponses { get; set; } = new List<RequestedStockResponse>();
		public List<RequestedItemsResponse> RequestedItemsResponse { get; set; } = new List<RequestedItemsResponse>();

		public bool IsNetworkLogonRequired { get; set; }
		public bool IsInternetRequired { get; set; }

		public bool IsActiveDirectoryRequired { get; set; }
		public string ActiveDirectory { get; set; }
		public bool IsMsExchangeRequired { get; set; }
		public string SharedFolders { get; set; }
		public bool IsMsTeamsAccountRequired { get; set; }
		public List<AssociatedAsset> AssociatedAssets { get; set; } = new List<AssociatedAsset>();
	}

	public class AssociatedAsset
	{
		public long AssetId { get; set; }
		public string Name { get; set; }
		public string AssignedTo { get; set; }
		public bool IsAssigned { get; set; }
		public string RegistrationNumber { get; set; }
		public string SerialNumber { get; set; }
		public string Category { get; set; }
		public string MakeName { get; set; }
		public string ModelName { get; set; }
		public string Description { get; set; }
		public long CategoryId { get; set; }
	}
}
