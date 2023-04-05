using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Enums;
using VMMCStockManagement.Domain.Models.Requests;

namespace VMMCStockManagement.Domain.Entities
{
	public class StockRequest : BaseEntity
	{
		public string? RequesterId { get; set; }
		public User? Requester { get; set; }
		public string? ReferenceNumber { get; set; }
		public string TicketNumber { get; set; }
		public string? OtherReason { get; set; }
		public long JobTitleId { get; set; }
		public JobTitle? JobTitle { get; set; }
		public long GrantId { get; set; }
		public Grant? Grant { get; set; }

		public long? ReasonId { get; set; }
		public Reason? Reason { get; set; }
		public long DepartmentId { get; set; }
		public Department? Department { get; set; }
		public string PhoneNumber { get; set; }
		public string ManagerId { get; set; }
		public User Manager { get; set; }
		public long FacilityId { get; set; }
		public Facility? Facility { get; set; }
		public DateTime? DateRequired { get; set; }
		public string Email { get; set; }
		//public List<StockRequestHardware> StockRequestHardware { get; set; }
		public List<StockRequestAssetCategory> StockRequestAssetCategories { get; set; } = new List<StockRequestAssetCategory>();
		public string? SpecifyOtherHardware { get; set; }
		public int? QtyOther { get; set; }
		public bool IsHardwareOther { get; set; }
		public bool IsNetworkLogonAccount { get; set; }
		public bool IsInternetAccess { get; set; }
		public bool IsActiveDirectory { get; set; }
		public bool IsMSExchangeAccount { get; set; }
		public bool IsMicrosoftTeamsAccount { get; set; }
		public string? SpecifyOtherUserCreationAccess { get; set; }
		public string? SharedFoldersAccessRequired { get; set; }
		public RequestApproval RequestApproval { get; set; }
		public RequestType RequestType { get; set; }
		public long? CountryId { get; set; }
		public Country? Country { get; set; }

		public string? EmployeeNumber { get; set; }
		public string? SignatureFileName { get; set; }
		public StockStatus? RequestStatus { get; set; } = Enums.StockStatus.Pending;

		public static StockRequest Create(StockRequestRequest request)
		{
			var items = new List<StockRequestAssetCategory>();

			if (request.SelectedAssetCategories != null)
			{
				foreach (var assetCategory in request.SelectedAssetCategories)
				{
					var StockRequestAssetCategoryRequest = new StockRequestAssetCategoryRequest
					{
						StockCategoryId = assetCategory.AssetCategoryId,
						UserId = request.UserId,
						Qty = assetCategory.Qty,
						HardwareSpecification = assetCategory.HardwareSpecification,
						FileName = assetCategory.FileName

					};

					var stockRequestAssetCategory = StockRequestAssetCategory
						.Create(StockRequestAssetCategoryRequest);

					items.Add(stockRequestAssetCategory);

				}
			}

			return new StockRequest
			{
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now,
				CreatedBy = request.UserId,
				UpdatedBy = request.UserId,
				ReferenceNumber = request.ReferenceNumber,
				TicketNumber = request.TicketNumber,
				OtherReason = request.OtherReason,
				GrantId = request.GrantId,
				DepartmentId = request.DepartmentId,
				PhoneNumber = request.PhoneNumber,
				ManagerId = request.ManagerId,
				FacilityId = request.FacilityId,
				JobTitleId = request.JobTitleId,
				DateRequired = request.DateRequired,
				Email = request.Email,
				SpecifyOtherHardware = request.SpecifyOtherHardware,

				QtyOther = request.QtyOther,
				IsHardwareOther = request.IsHardwareOther,
				IsNetworkLogonAccount = request.IsNetworkLogonAccount,
				IsInternetAccess = request.IsInternetAccess,
				IsActiveDirectory = request.IsActiveDirectory,
				IsMSExchangeAccount = request.IsMSExchangeAccount,
				IsMicrosoftTeamsAccount = request.IsMicrosoftTeamsAccount,
				SpecifyOtherUserCreationAccess = request.SpecifyOtherUserCreationAccess,
				SharedFoldersAccessRequired = request.SharedFoldersAccessRequired,
				StockRequestAssetCategories = items,
				RequestType = request.RequestType,
				CountryId = request.CountryId,
				EmployeeNumber = request.EmployeeNumber,
				RequesterId = request.RequesterId,
				ReasonId = request.ReasonId == -1 ? null : request.ReasonId,

			};
		}
	}
}
