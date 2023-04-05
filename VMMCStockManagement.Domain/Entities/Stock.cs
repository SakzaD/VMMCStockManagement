using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Enums;
using VMMCStockManagement.Domain.Models.Requests;

namespace VMMCStockManagement.Domain.Entities
{
	public class Stock : BaseEntity
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string RegistrationNumber { get; set; }
		public string SerialNumber { get; set; }
		public long? CategoryId { get; set; }
		public Category? Category { get; set; }
		public long? ModelId { get; set; }
		public Model? Model { get; set; }
		public long? GrantId { get; set; }
		public Grant? Grant { get; set; }
		public bool IsAddedToRequest { get; set; }
		public string? ReferenceNumber { get; set; }

		public string? InvoiceNumber { get; set; }

		[Precision(18, 2)]
		public decimal? PurchasePrice { get; set; }
		public WarrantyType WarrantyType { get; set; }
		public DateTime? WarrantStartDate { get; set; }
		public DateTime? WarrantEndDate { get; set; }
		public DateTime? TransferDate { get; set; }
		public DateTime? PurchaseOrReceivedDate { get; set; }
		public long? SupplierId { get; set; }
		public Supplier? Supplier { get; set; }
		public long? FacilityId { get; set; }
		public Facility? Facility { get; set; }
		public long? DepartmentId { get; set; }
		public Department? Department { get; set; }
		public UploadType? UploadType { get; set; }

		public static Stock Create(StockEntityRequest request)
		{
			return new Stock
			{
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now,
				UploadType = request.UploadType,
				CreatedBy = request.UserId,
				UpdatedBy = request.UserId,
				Name = request.Name,
				Description = request.Description,
				RegistrationNumber = string.IsNullOrEmpty(request.RegistrationNumber) ?
				request.RegistrationNumber : request.RegistrationNumber.Trim(),
				SerialNumber = string.IsNullOrEmpty(request.SerialNumber) ?
				request.SerialNumber : request.SerialNumber.Trim(),
				CategoryId = request.CategoryId,
				GrantId = request.GrantId,
				ReferenceNumber = string.IsNullOrEmpty(request.ReferenceOrTicketNumber) ?
				request.ReferenceOrTicketNumber : request.ReferenceOrTicketNumber.Trim(),
				IsAddedToRequest = request.IsAddedToRequest,
				ModelId = request.ModelId,
				WarrantyType = request.WarrantyType,
				PurchasePrice = request.PurchasePrice,
				TransferDate = request.TransferDate,
				SupplierId = request.SupplierId,
				InvoiceNumber = request.InvoiceNumber,
				WarrantStartDate = request.WarrantStartDate,
				WarrantEndDate = request.WarrantEndDate,
				PurchaseOrReceivedDate = request.PurchaseOrReceivedDate,
				FacilityId = request.LocationId,
				DepartmentId = request.DepartmentId,
			};
		}

	}
}
