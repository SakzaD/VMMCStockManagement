using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Enums;

namespace VMMCStockManagement.Domain.Models.Responses.QueryResponse.Stock
{
	public class StockListResponse
	{
		public long Id { get; set; }
		public long? CategoryId { get; set; }
		public long? ModelId { get; set; }
		public long? MakeId { get; set; }
		public long? GrantId { get; set; }
		public bool IsAddedToRequest { get; set; }
		public string ReferenceOrTicketNumber { get; set; }
		public string ReferenceNumber { get; set; }
		public string MakeName { get; set; }
		public string ModelName { get; set; }
		public string GrantName { get; set; }
		public string AssetCategoryName { get; set; }

		public string Name { get; set; }
		public string Description { get; set; }
		public string Category { get; set; }
		public string SerialNumber { get; set; }
		public string RegistrationNumber { get; set; }

		public string? InvoiceNumber { get; set; }
		public long? SupplierId { get; set; }
		public WarrantyType WarrantyType { get; set; }
		public DateTime? WarrantStartDate { get; set; }
		public DateTime? WarrantEndDate { get; set; }
		public string? PurchaseOrReceivedDate { get; set; }
		public string? TransferDate { get; set; }
		public decimal? PurchasePrice { get; set; }
		public long? LocationId { get; set; }
		public long? DepartmentId { get; set; }
	}
}
