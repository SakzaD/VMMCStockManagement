using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Enums;
using VMMCStockManagement.Domain.Models.Requests;

namespace VMMCStockManagement.Domain.Entities
{
	public class Reason : BaseEntity
	{
		public string Description { get; set; }
		public long? ReasonCategoryId { get; set; }
		public ReasonCategory? ReasonCategory { get; set; }
		public LookupType LookupType { get; set; }

		public static Reason Create(ReasonRequest request)
		{
			return new Reason
			{
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now,
				CreatedBy = request.UserId,
				UpdatedBy = request.UserId,
				Description = request.Description,
				ReasonCategoryId = request.ReasonCategoryId,
				LookupType = LookupType.Normal
			};
		}
	}
}
