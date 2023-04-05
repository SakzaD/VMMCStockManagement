using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Models.Requests;

namespace VMMCStockManagement.Domain.Entities
{
	public class ReasonCategory : BaseEntity
	{
		public string Description { get; set; }

		public static ReasonCategory Create(ReasonCategoryRequest request)
		{
			return new ReasonCategory
			{
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now,
				CreatedBy = request.UserId,
				UpdatedBy = request.UserId,
				Description = request.Description
			};
		}
	}
}
