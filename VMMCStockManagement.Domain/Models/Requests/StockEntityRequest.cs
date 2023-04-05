using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Enums;

namespace VMMCStockManagement.Domain.Models.Requests
{
	public class StockEntityRequest : BaseRequest
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string RegistrationNumber { get; set; }
		public string SerialNumber { get; set; }
		public long? CategoryId { get; set; }
		public long? ModelId { get; set; }
		public bool IsAddedToRequest { get; set; }
		public long? GrantId { get; set; }
		public string? ReferenceOrTicketNumber { get; set; }
		public WarrantyType WarrantyType { get; set; }
		public DateTime? WarrantStartDate { get; set; }
		public DateTime? WarrantEndDate { get; set; }
		public DateTime? TransferDate { get; set; }
		public DateTime? PurchaseOrReceivedDate { get; set; }
		public decimal? PurchasePrice { get; set; }
		public long? SupplierId { get; set; }
		public string? InvoiceNumber { get; set; }
		public long? LocationId { get; set; }
		public long? DepartmentId { get; set; }
		public UploadType? UploadType { get; set; }
	}
}
