using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Enums;

namespace VMMCStockManagement.Domain.Models.Responses.QueryResponse.StockRequest
{
	public class StockRequestByIdResponse
	{
		public string? ReferenceNumber { get; set; }
		public string TicketNumber { get; set; }
		public string? OtherReason { get; set; }
		public long JobTitleId { get; set; }
		public long GrantId { get; set; }
		public long DepartmentId { get; set; }
		public string Phone { get; set; }
		public string ManagerId { get; set; }
		public long FacilityId { get; set; }
		public long? CountryId { get; set; }
		public string? DateRequired { get; set; }
		public string Email { get; set; }
		//public List<long>? SelectedAssetCategories { get; set; }
		//public List<SelectedAsset>? SelectedAssetCategories { get; set; }
		public string? SpecifyOtherHardware { get; set; }
		public bool IsHardwareOther { get; set; }
		public bool IsNetworkLogonAccount { get; set; }
		public bool IsInternetAccess { get; set; }
		public bool IsActiveDirectory { get; set; }
		public bool IsMSExchangeAccount { get; set; }
		public bool IsMicrosoftTeamsAccount { get; set; }
		public string? SpecifyOtherUserCreationAccess { get; set; }
		public string? SharedFoldersAccessRequired { get; set; }
		public IFormFile? OtherFile { get; set; }
		public RequestType RequestType { get; set; }
		public string? EmployeeNumber { get; set; }
		public string? RequesterId { get; set; }
		public long? ReasonId { get; set; }
	}
}
