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
				stockTracker.Supplier = new SupplierModel();
				stockTracker.DataCapure = new DataCapureModel();
				//stockTracker.ItManager = new ItManagerModel();
				//stockTracker.ItStaff = new ItStaffModel();
				return stockTracker;
			}

			var dateArrived = stockRequest.CreatedAt.ToString(dateFormat);
			var dateApproved = approvalRequest.LineManagerApprovalDate != null ?
				approvalRequest.LineManagerApprovalDate.Value.ToString(dateFormat) : "Pending";

			var supplier = new SupplierModel
			{
				DateArrived = dateArrived,
				DateApproved = dateApproved,
				IsCompleted = approvalRequest.LineManagerStatus == Enums.StockStatus.Approved
			};

			dateArrived = approvalRequest.LineManagerApprovalDate != null ?
			   approvalRequest.LineManagerApprovalDate.Value.ToString(dateFormat) : "Pending";
			
			dateApproved = approvalRequest.ItManagerApprovalDate != null ?
			   approvalRequest.ItManagerApprovalDate.Value.ToString(dateFormat) : "Pending";

			var dataCapture = new DataCapureModel
			{
				DateArrived = dateArrived,
				DateApproved = dateApproved,
				IsCompleted = approvalRequest.ItManagerStatus == Enums.StockStatus.Approved
			};


			dateArrived = approvalRequest.ItManagerApprovalDate != null ?
		   approvalRequest.ItManagerApprovalDate.Value.ToString(dateFormat) : "Pending";
			dateApproved = approvalRequest.ItStaffCompleteDate != null ?
			   approvalRequest.ItStaffCompleteDate.Value.ToString(dateFormat) : "Pending";

			//var itStaff = new ItStaffModel
			//{
			//	DateArrived = dateArrived,
			//	DateCompleted = dateApproved,
			//	IsCompleted = approvalRequest.ItStaffStatus == Enums.StockStatus.Completed
			//};

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
			stockTracker.DataCapure = dataCapture;
			stockTracker.Supplier = supplier;
			//stockTracker.ItManager = itManager;
			//stockTracker.ItStaff = itStaff;
			stockTracker.RequestedItems = items;


			return stockTracker;
		}
	}
}
