using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Enums;
using VMMCStockManagement.Domain.Models.Requests;

namespace VMMCStockManagement.Domain.Entities
{
	public class RequestApproval : BaseEntity
	{
		public StockStatus InitiatorStatus { get; set; }
		public string InitiatorId { get; set; }

		public StockStatus LineManagerStatus { get; set; }
		public string? LineManagerApprovalReason { get; set; }
		public string? LineManagerRejectionReason { get; set; }
		public string? LineManagerId { get; set; }
		public DateTime? LineManagerApprovalDate { get; set; }	

		public StockStatus ItManagerStatus { get; set; }
		public string? ItManagerApprovalReason { get; set; }
		public string? ItManagerRejectionReason { get; set; }
		public string? ItManagerId { get; set; }
		public DateTime? ItManagerApprovalDate { get; set; }

		public StockStatus? ItStaffStatus { get; set; }
		public string? ItStaffId { get; set; }
		public DateTime? ItStaffCompleteDate { get; set; }


		[ForeignKey("AssetRequest")]
		public long StockRequestId { get; set; }


		public static RequestApproval Create(RequestApprovalRequest request, long stockRequestId)
		{
			return new RequestApproval
			{
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now,
				CreatedBy = request.UserId,
				UpdatedBy = request.UserId,
				InitiatorId = request.UserId,
				InitiatorStatus = StockStatus.Approved,
				LineManagerStatus = StockStatus.Pending,
				//ServiceDeskStatus = RequestStatus.Pending,
				ItManagerStatus = StockStatus.Pending,
				ItStaffStatus = StockStatus.Pending,
				StockRequestId = stockRequestId
			};
		}

	}
}
