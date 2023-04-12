using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Enums;

namespace VMMCStockManagement.Domain.Models.Responses.QueryResponse.Stock
{
	public class StockResponse
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string RegistrationNumber { get; set; }
		public string SerialNumber { get; set; }
		public long? CategoryId { get; set; }
		public long? MakeId { get; set; }
		public string? MakeName { get; set; }
		public string? ModelName { get; set; }
		public long? ModelId { get; set; }
		public long? SupplierId { get; set; }
		public WarrantyType warrantyType { get; set; }
		public string Description { get; set; }
		public decimal? PurchasePrice { get; set; }
		public string? PurchaseOrReceivedDate { get; set; }
		public string TransferDate { get; set; }
		public long? GrantId { get; set; }
		public long? LocationId { get; set; }
		public long? DepartmentId { get; set; }
		public string? InvoiceNumber { get; set; }
		public bool? IsAddedToRequest { get; set; }
		public string? ReferenceOrTicketNumber { get; set; }
		public string? WarrantStartDate { get; set; }
		public string? WarrantEndDate { get; set; }
	}
}
