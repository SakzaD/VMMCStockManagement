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

		public StockStatus DistrictCoordinatorStatus { get; set; }
		public string? DistrictCoordinatorApprovalReason { get; set; }
		public string? DistrictCoordinatorRejectionReason { get; set; }
		public string? DistrictCoordinatorId { get; set; }
		public DateTime? DistrictCoordinatorApprovalDate { get; set; }	

		public StockStatus ProgramAdministratorStatus { get; set; }
		public string? ProgramAdministratorApprovalReason { get; set; }
		public string? ProgramAdministratorRejectionReason { get; set; }
		public string? ProgramAdministratorId { get; set; }
		public DateTime? ProgramAdministratorApprovalDate { get; set; }

		public StockStatus? HOApproverStatus { get; set; }
		public string? HOApproverId { get; set; }
		public DateTime? HOApproverCompleteDate { get; set; }


		[ForeignKey("StockRequest")]
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
				DistrictCoordinatorStatus = StockStatus.Pending,			
				ProgramAdministratorStatus = StockStatus.Pending,
				HOApproverStatus = StockStatus.Pending,
				StockRequestId = stockRequestId
			};
		}

	}
}
