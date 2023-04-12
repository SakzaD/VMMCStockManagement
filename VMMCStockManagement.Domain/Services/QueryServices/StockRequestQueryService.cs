using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Enums;
using VMMCStockManagement.Domain.Interfaces.Repositories;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.RequestStock;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.StockRequest;
using VMMCStockManagement.Domain.Models.Responses;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.Stock;
using VMMCStockManagement.Domain.Interfaces.Services.QueryServices;

namespace VMMCStockManagement.Domain.Services.QueryServices
{
	public class StockRequestQueryService : BaseQueryService<StockRequestFilter, StockRequest>, IStockRequestQueryService
	{
		private readonly IQueryRepository<StockByReference, StockByReferenceFilter> assetByReferenceQueryRepository;
		private readonly UserManager<User> userManager;
		private readonly IQueryRepository<UserAsset, UserAssetFilter> userAssetQueryRepository;
		public StockRequestQueryService(IQueryRepository<StockRequest, StockRequestFilter> queryRepository,
			IQueryRepository<StockByReference, StockByReferenceFilter> assetByReferenceQueryRepository,
			ILogger<BaseService> logger, IQueryRepository<UserAsset, UserAssetFilter> userAssetQueryRepository,
			UserManager<User> userManager) : base(queryRepository, logger)
		{
			this.assetByReferenceQueryRepository = assetByReferenceQueryRepository;
			this.userManager = userManager;
			this.userAssetQueryRepository = userAssetQueryRepository;
		}

		public override string ServiceName => nameof(StockRequestQueryService);
		public StockStatus GetStatus(RequestApproval? requestApproval)
		{
			if (requestApproval == null)
				return StockStatus.Pending;

			if (requestApproval.DistrictCoordinatorStatus == StockStatus.Pending)
			{
				return StockStatus.Pending;
			}
			else if (requestApproval.DistrictCoordinatorStatus == StockStatus.Rejected)
			{
				return StockStatus.Rejected;
			}
			else if (requestApproval.DistrictCoordinatorStatus == StockStatus.Amend)
			{
				return StockStatus.Amend;
			}

			if (requestApproval.ProgramAdministratorStatus == StockStatus.Pending)
			{
				return StockStatus.Pending;
			}
			else if (requestApproval.ProgramAdministratorStatus == StockStatus.Rejected)
			{
				return StockStatus.Rejected;
			}
			else if (requestApproval.ProgramAdministratorStatus == StockStatus.Amend)
			{
				return StockStatus.Amend;
			}

			if (requestApproval.HOApproverStatus == StockStatus.Pending)
			{
				return StockStatus.Pending;
			}
			else if (requestApproval.HOApproverStatus == StockStatus.Rejected)
			{
				return StockStatus.Rejected;
			}
			else if (requestApproval.HOApproverStatus == StockStatus.Amend)
			{
				return StockStatus.Amend;
			}

			if (requestApproval.HOApproverStatus == StockStatus.Completed)
			{
				return StockStatus.Completed;
			}

			return StockStatus.Pending;
		}
		public string GetApprovalStatus(RequestApproval? requestApproval)
		{
			if (requestApproval == null)
				return "Pending";

			if (requestApproval.DistrictCoordinatorStatus == StockStatus.Pending)
			{
				return "At Line Manager";
			}
			else if (requestApproval.DistrictCoordinatorStatus == StockStatus.Rejected)
			{
				return "Declined";
			}
			else if (requestApproval.DistrictCoordinatorStatus == StockStatus.Amend)
			{
				return "Amend required";
			}			

			if (requestApproval.ProgramAdministratorStatus == StockStatus.Pending)
			{
				return "At IT Manager";
			}
			else if (requestApproval.ProgramAdministratorStatus == StockStatus.Rejected)
			{
				return "Declined";
			}
			else if (requestApproval.ProgramAdministratorStatus == StockStatus.Amend)
			{
				return "Amend required";
			}

			if (requestApproval.HOApproverStatus == StockStatus.Pending)
			{
				return "At IT Staff";
			}


			if (requestApproval.HOApproverStatus == StockStatus.Approved)
			{
				return "Pending Allocation";
			}


			if (requestApproval.HOApproverStatus == StockStatus.Completed)
			{
				return "Completed";
			}


			return "Pending";
		}
		public StockStatus GetRequestStatus(long requestId)
		{
			var data = queryRepository.GetById(requestId);
			return GetStatus(data.RequestApproval);
		}
		public bool CanApprove(RequestApproval? requestApproval, AccessRole currentRole)
		{
			if (requestApproval == null)
				return true;

			if (requestApproval.DistrictCoordinatorStatus != StockStatus.Approved
				&& currentRole == AccessRole.DistrictCoordinator)
			{
				return true;
			}			

			if (requestApproval.ProgramAdministratorStatus != StockStatus.Approved
				&& currentRole == AccessRole.ProgramAdministrator)
			{
				return true;
			}

			if (requestApproval.HOApproverStatus != StockStatus.Approved
				&& currentRole == AccessRole.HOApprover)

			{
				return true;
			}


			return false;
		}
		public AccessRole GetAccessRole(RequestApproval? requestApproval)
		{
			if (requestApproval == null)
				return AccessRole.Unknown;

			if (requestApproval.DistrictCoordinatorStatus != StockStatus.Approved)
			{
				return AccessRole.DistrictCoordinator;
			}
		
			if (requestApproval.ProgramAdministratorStatus != StockStatus.Approved)
			{
				return AccessRole.ProgramAdministrator;
			}

			if (requestApproval.HOApproverStatus != StockStatus.Approved)
			{
				return AccessRole.HOApprover;
			}
			return AccessRole.Unknown;
		}
		public ObjectListResponse<StockRequestListResponse> Filter(StockRequestFilter filter)
		{
			var response = new ObjectListResponse<StockRequestListResponse>();
			var data = queryRepository.Filter(filter).ToList();

			var mappedData = new List<StockRequestListResponse>();


			foreach (var item in data)
			{
				var ticketNumber = item.TicketNumber;

				var manager = item.Manager == null ? "Unknown" : item.Manager.FullName;

				var statusDescription = GetApprovalStatus(item.RequestApproval);
				var status = GetStatus(item.RequestApproval).ToString();
				
				var StockRequestAssetCategories = new List<RequestedStockResponse>();

				if (item.StockRequestAssetCategories != null)
				{
					foreach (var category in item.StockRequestAssetCategories)
					{
						StockRequestAssetCategories.Add(new RequestedStockResponse
						{
							Id = category.Id,
							Name = category.Category.Name,
							Qty = category.Qty,
							HardwareSpecification = category.HardwareSpecification,
						});
					}
				}

				mappedData.Add(new StockRequestListResponse
				{
					Id = item.Id,
					CanAction = CanApprove(item.RequestApproval, filter.AccessRole),
					StatusDescription = statusDescription,
					Status = status,
					DateCreated = item.CreatedAt.ToString("yyyy-MM-dd HH:mm"),
					TicketNumber = item.TicketNumber,
					ReferenceNumber = item.ReferenceNumber,
					JobTitle = item?.JobTitle.Name,
					Grant = item?.Grant.Name,
					Department = item?.Department.Name,
					Email = item.Email,
					ManagerName = manager,
					Phone = item.PhoneNumber,
					DateRequired = item.DateRequired.Value.ToString("yyyy-MM-dd"),
					OtherReason = item.OtherReason,
					Facility = item?.Facility.Name,
					RequestedStockResponses = StockRequestAssetCategories


				});
			}
			response.Data = mappedData;
			response.CodeStatus = ResponseStatus.Success;

			return response;
		}
		public ObjectResponse<StockRequestByIdResponse> GetRequestById(long id)
		{
			var response = new ObjectResponse<StockRequestByIdResponse>();

			var assetRequest = queryRepository.GetById(id);
			
			var mappedData = new StockRequestByIdResponse
			{

				ReferenceNumber = assetRequest.ReferenceNumber,
				TicketNumber = assetRequest.TicketNumber,
				OtherReason = assetRequest.OtherReason,
				JobTitleId = assetRequest.JobTitleId,
				GrantId = assetRequest.GrantId,
				DepartmentId = assetRequest.DepartmentId,
				Phone = assetRequest.PhoneNumber,
				ManagerId = assetRequest.ManagerId,
				FacilityId = assetRequest.FacilityId,
				CountryId = assetRequest.CountryId,
				DateRequired = assetRequest.DateRequired.Value.ToString("yyyy-MM-dd"),
				Email = assetRequest.Email,
				SpecifyOtherHardware = assetRequest.SpecifyOtherHardware,
				IsHardwareOther = assetRequest.IsHardwareOther,
				IsNetworkLogonAccount = assetRequest.IsNetworkLogonAccount,
				IsInternetAccess = assetRequest.IsInternetAccess,
				IsActiveDirectory = assetRequest.IsActiveDirectory,
				IsMSExchangeAccount = assetRequest.IsMSExchangeAccount,
				IsMicrosoftTeamsAccount = assetRequest.IsMicrosoftTeamsAccount,
				SpecifyOtherUserCreationAccess = assetRequest.SpecifyOtherUserCreationAccess,
				SharedFoldersAccessRequired = assetRequest.SharedFoldersAccessRequired,
				RequestType = assetRequest.RequestType,
				EmployeeNumber = assetRequest.EmployeeNumber,
				RequesterId = assetRequest.RequesterId,
			};
			response.Data = mappedData;
			response.CodeStatus = ResponseStatus.Success;

			return response;
		}

		private string GetReason(StockRequest assetRequest, AccessRole accessRole)
		{

			string reason = string.Empty;

			if (accessRole == AccessRole.DistrictCoordinator)
			{
				reason = assetRequest.RequestApproval.DistrictCoordinatorApprovalReason;
			}
			else if (accessRole == AccessRole.ProgramAdministrator)
			{
				reason = assetRequest.RequestApproval.ProgramAdministratorApprovalReason;
			}
			else
			{

				if (assetRequest.ReasonId != null)
				{
					reason = assetRequest.Reason.Description;
				}
				else
				{
					reason = assetRequest.OtherReason;
				}
			}

			return reason;

		}

		public ObjectResponse<StockGrantResponse> GetGrantByReference(string? referenceNumber)
		{

			var response = new ObjectResponse<StockGrantResponse>();

			var assetRequest = queryRepository
				.GetAll()
				.FirstOrDefault(x => x.ReferenceNumber == referenceNumber);

			if (assetRequest != null)
			{
				var data = new StockGrantResponse
				{
					GrantId = assetRequest.GrantId,
				};
				response.CodeStatus = ResponseStatus.Success;
				response.Data = data;
			}

			return response;
		}

		public ObjectResponse<StockRequestResponse> Get(StockRequestFilter filter)
		{
			var assetRequest = queryRepository.GetById(filter.Id);
			var response = new ObjectResponse<StockRequestResponse>();

			var mappedData = new StockRequestResponse();


			var manager = assetRequest.Manager == null ? "Unknown" : assetRequest.Manager.FullName;
			var status = GetApprovalStatus(assetRequest.RequestApproval);

			StockByReferenceFilter StockByReferenceFilter = new StockByReferenceFilter
			{
				ReferenceNumber = assetRequest.ReferenceNumber,
			};

			var associatedAssets = assetByReferenceQueryRepository
				.Filter(StockByReferenceFilter)
				.ToList();

			List<AssociatedAsset> associatedAssetsResponse = new List<AssociatedAsset>();

			if (associatedAssets != null && associatedAssets.Count() > 0)
			{
				foreach (var assetByReference in associatedAssets)
				{
					var userAsset = userAssetQueryRepository
						.GetAll()
						.FirstOrDefault(x => x.AssetId == assetByReference.AssetId && x.Status == EntityStatus.Active);

					bool isAssigned = userAsset != null;

					var assignedTo = "Not Assigned";

					if (isAssigned)
					{
						userAsset = userAssetQueryRepository.GetById(userAsset.Id);
						assignedTo = userAsset.User.FullName;
					}
					associatedAssetsResponse.Add(new AssociatedAsset
					{
						IsAssigned = isAssigned,
						AssignedTo = assignedTo,
						AssetId = assetByReference.AssetId,
						Name = assetByReference.Stock?.Name,
						RegistrationNumber = assetByReference.Stock?.RegistrationNumber,
						SerialNumber = assetByReference.Stock?.SerialNumber,
						Category = assetByReference.Stock?.Category.Name,
						MakeName = assetByReference.Stock?.Model.Make.Name,
						ModelName = assetByReference.Stock?.Model.Name,
						Description = assetByReference.Stock?.Description,
						CategoryId = assetByReference.Stock.CategoryId.Value
					});
				}
			}

			var StockRequestAssetCategories = new List<RequestedStockResponse>();

			if (assetRequest.StockRequestAssetCategories != null)
			{
				foreach (var category in assetRequest.StockRequestAssetCategories)
				{
					int assignedNumber = associatedAssetsResponse.Count(x => x.CategoryId == category.Category.Id && x.IsAssigned);
					StockRequestAssetCategories.Add(new RequestedStockResponse
					{
						Id = category.Id,
						Name = category.Category.Name,
						Qty = category.Qty,
						AssignedQty = assignedNumber,
						HardwareSpecification = category.HardwareSpecification,
						FileName = category.FileName,
					});
				}
			}

			string reason = GetReason(assetRequest, filter.AccessRole);


			mappedData = new StockRequestResponse
			{
				Id = assetRequest.Id,
				StatusDescription = status,
				Status = GetStatus(assetRequest.RequestApproval).ToString(),
				AccessRole = GetAccessRole(assetRequest.RequestApproval),
				DateCreated = assetRequest.CreatedAt.ToString("yyyy-MM-dd HH:mm"),
				TicketNumber = assetRequest.TicketNumber,
				ReferenceNumber = assetRequest.ReferenceNumber,
				JobTitle = assetRequest?.JobTitle.Name,
				Grant = assetRequest?.Grant.Name,
				Department = assetRequest?.Department.Name,
				Email = assetRequest.Email,
				Manager = manager,
				Phone = assetRequest.PhoneNumber,
				DateRequired = assetRequest.DateRequired.Value.ToString("yyyy-MM-dd"),
				Reason = reason,
				Facility = assetRequest?.Facility.Name,
				Country = assetRequest?.Country.Name,
				ActiveDirectory = assetRequest.SpecifyOtherUserCreationAccess,
				SharedFolders = assetRequest.SharedFoldersAccessRequired,
				EmployeeNumber = assetRequest?.Requester.EmployeeNumber,
				FirstName = assetRequest?.Requester.FirstName,
				LastName = assetRequest?.Requester.LastName,
				RequestedStockResponses = StockRequestAssetCategories,
				IsActiveDirectoryRequired = assetRequest.IsActiveDirectory,
				IsInternetRequired = assetRequest.IsInternetAccess,
				IsNetworkLogonRequired = assetRequest.IsNetworkLogonAccount,
				IsMsExchangeRequired = assetRequest.IsMSExchangeAccount,
				IsMsTeamsAccountRequired = assetRequest.IsMicrosoftTeamsAccount,
				AssociatedAssets = associatedAssetsResponse
			};

			response.Data = mappedData;
			response.CodeStatus = ResponseStatus.Success;

			return response;
		}
		public ObjectResponse<NotificationResponse> GetRequestNumber(StockRequestFilter filter)
		{
			var response = new ObjectResponse<NotificationResponse>();
			var data = queryRepository.Filter(filter);
			int count = 0;


			if (filter.AccessRole == AccessRole.DistrictCoordinator)

				count = data.Count(x => x.RequestApproval.DistrictCoordinatorStatus == StockStatus.Pending);			

			else if (filter.AccessRole == AccessRole.ProgramAdministrator)

				count = data.Count(x => x.RequestApproval.DistrictCoordinatorStatus == StockStatus.Approved
				&& x.RequestApproval.ProgramAdministratorStatus == StockStatus.Pending);

			else if (filter.AccessRole == AccessRole.HOApprover)

				count = data.Count(x => x.RequestApproval.DistrictCoordinatorStatus == StockStatus.Approved
				&& x.RequestApproval.ProgramAdministratorStatus == StockStatus.Approved
				&& x.RequestApproval.HOApproverStatus == StockStatus.Pending);


			var notification = new NotificationResponse
			{
				NumberOfRequest = count
			};


			response.Data = notification;
			response.CodeStatus = ResponseStatus.Success;

			return response;
		}

		public ObjectResponse<StockRequestResponse> SearchRequestByReferenceNumber(string referenceNumber)
		{
			var response = new ObjectResponse<StockRequestResponse>();


			try
			{

				var data = queryRepository
					.GetAll()
					.FirstOrDefault(x => x.ReferenceNumber == referenceNumber);

				if (data == null)
				{

					var request = queryRepository.GetById(data.Id);

					response.Data = new StockRequestResponse
					{

					};
				}
				else
				{

				}

				response.CodeStatus = ResponseStatus.Success;
			}
			catch (Exception ex)
			{

			}

			return response;
		}

		public ObjectResponse<StockRequestManagerResponse> GetNoneCompletedRequestById(long requestId)
		{
			var response = new ObjectResponse<StockRequestManagerResponse>();

			try
			{

				var request = queryRepository.GetById(requestId);

				var StockRequestManagerResponse = new StockRequestManagerResponse
				{

					Requester = request.Requester.FullName,
					EmployeeNumber = request.Requester.EmployeeNumber,
					Grant = request.Grant.Name,
					GrantId = request.Grant.Id,
					ReferenceNumber = request.ReferenceNumber,
					TicketNumber = request.TicketNumber,

				};

				var requestedItems = new List<RequestedItem>();

				foreach (var category in request.StockRequestAssetCategories)
				{
					requestedItems.Add(new RequestedItem
					{
						CategoryId = category.Id,
						Description = category.Category.Name,
						RequestedQty = category.Qty
					});
				}

				StockByReferenceFilter StockByReferenceFilter = new StockByReferenceFilter
				{
					ReferenceNumber = request.ReferenceNumber,
				};

				var associatedAssets = assetByReferenceQueryRepository
					.Filter(StockByReferenceFilter)
					.ToList();

				if (associatedAssets != null && associatedAssets.Count() > 0)
				{

					for (int i = 0; i < requestedItems.Count(); i++)
					{
						requestedItems[i].AssignedQty = associatedAssets.Count(x => x.Stock.CategoryId == requestedItems[i].CategoryId);
					}

				}

				StockRequestManagerResponse.RequestedItems = requestedItems;

				response.Data = StockRequestManagerResponse;
				response.CodeStatus = ResponseStatus.Success;
			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message, ex);
			}

			return response;

		}

		public ObjectListResponse<StockRequestManagerListResponse> GetAllNoneCompletedRequests()
		{
			var response = new ObjectListResponse<StockRequestManagerListResponse>();

			try
			{

				var StockRequestManagerListResponse = new List<StockRequestManagerListResponse>();

				var requests = queryRepository
					.GetAll()
					.Where(x => x.RequestApproval.HOApproverStatus != Enums.StockStatus.Completed)
					.ToList();

				foreach (var item in requests)
				{

					var request = queryRepository.GetById(item.Id);

					StockRequestManagerListResponse.Add(new StockRequestManagerListResponse
					{
						Id = request.Id,
						DateRequested = request.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"),
						ReferenceNumber = request.ReferenceNumber,
						TicketNumber = request.TicketNumber,
						Requester = request.Requester.FullName,
						EmployeeNumber = request.Requester.EmployeeNumber,
						NumberOfAssets = request.StockRequestAssetCategories.Sum(x => x.Qty),
					});
				}

				response.Data = StockRequestManagerListResponse;
				response.CodeStatus = ResponseStatus.Success;
			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message, ex);
			}

			return response;
		}
	}
}
