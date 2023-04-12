using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Interfaces.Repositories;
using VMMCStockManagement.Domain.Interfaces;
using VMMCStockManagement.Domain.Models;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Models.Requests.Filters;

namespace VMMCStockManagement.Domain.Services
{
	public class TrackingService : ITrackingService
	{
		private readonly IQueryRepository<StockRequest, StockRequestFilter> stockRequestQueryRepo;

		public TrackingService(IQueryRepository<StockRequest, StockRequestFilter> stockRequestQueryRepo)
		{
			this.stockRequestQueryRepo = stockRequestQueryRepo;
		}

		public StockTracker? GetRequestStatus(long stockRequestId)
		{
			var stockTracker = new StockTracker();
			var stockRequest = stockRequestQueryRepo.GetById(stockRequestId);

			if (stockRequest == null) return null;

			var dateFormat = "ddd MMM dd, yyyy";

			stockTracker.ReferenceNumber = stockRequest.ReferenceNumber;
			stockTracker.TicketNumber = stockRequest.TicketNumber;

			var requester = new RequesterModel
			{
				DateRequested = stockRequest.CreatedAt.ToString(dateFormat),
				FullName = stockRequest.Requester.FullName,
				EmployeeNumber = stockRequest.EmployeeNumber,
				Email = stockRequest.Requester.Email,
			};


			var approvalRequest = stockRequest.RequestApproval;


			if (approvalRequest == null)
			{
				stockTracker.Requester = new RequesterModel();
				stockTracker.DistrictCoordinator = new DistrictCoordinatorModel();
				stockTracker.ProgramAdministrator = new ProgramAdministratorModel();
				stockTracker.HOApprover = new HOApproverModel();				
				return stockTracker;
			}

			var dateArrived = stockRequest.CreatedAt.ToString(dateFormat);
			var dateApproved = approvalRequest.DistrictCoordinatorApprovalDate != null ?
				approvalRequest.DistrictCoordinatorApprovalDate.Value.ToString(dateFormat) : "Pending";

			var districtCoordinator = new DistrictCoordinatorModel
			{
				DateArrived = dateArrived,
				DateApproved = dateApproved,
				IsCompleted = approvalRequest.DistrictCoordinatorStatus == Enums.StockStatus.Approved
			};

			dateArrived = approvalRequest.DistrictCoordinatorApprovalDate != null ?
			   approvalRequest.DistrictCoordinatorApprovalDate.Value.ToString(dateFormat) : "Pending";
			
			dateApproved = approvalRequest.DistrictCoordinatorApprovalDate != null ?
			   approvalRequest.DistrictCoordinatorApprovalDate.Value.ToString(dateFormat) : "Pending";

			var programAdministrator = new ProgramAdministratorModel
			{
				DateArrived = dateArrived,
				DateApproved = dateApproved,
				IsCompleted = approvalRequest.ProgramAdministratorStatus == Enums.StockStatus.Approved
			};


			dateArrived = approvalRequest.ProgramAdministratorApprovalDate != null ?
		   approvalRequest.ProgramAdministratorApprovalDate.Value.ToString(dateFormat) : "Pending";
			dateApproved = approvalRequest.HOApproverCompleteDate != null ?
			   approvalRequest.HOApproverCompleteDate.Value.ToString(dateFormat) : "Pending";

			var hoApprover = new HOApproverModel
			{
				DateArrived = dateArrived,
				DateApproved = dateApproved,
				IsCompleted = approvalRequest.HOApproverStatus == Enums.StockStatus.Completed
			};

			var items = new List<RequestedItemModel>();

			if (stockRequest.StockRequestAssetCategories != null)
			{
				foreach (var item in stockRequest.StockRequestAssetCategories)
				{
					items.Add(new RequestedItemModel
					{
						Name = item.Category.Name,
						Description = item.Category.Description,
						Qty = item.Qty
					});
				}

				if (stockRequest.IsHardwareOther)
				{
					items.Add(new RequestedItemModel
					{
						Name = "Other Specification",
						Description = stockRequest.SpecifyOtherHardware,
						Qty = stockRequest.QtyOther.Value
					});
				}
			}

			stockTracker.Requester = requester;
			stockTracker.DistrictCoordinator = districtCoordinator;
			stockTracker.ProgramAdministrator = programAdministrator;
			stockTracker.HOApprover = hoApprover;			
			stockTracker.RequestedItems = items;


			return stockTracker;
		}
	}
}
