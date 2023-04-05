using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Models.Requests;

namespace VMMCStockManagement.Domain.Entities
{
	public class Category : BaseEntity
	{
		public string Name { get; set; }
		public string Description { get; set; }

		public static Category Create(CategoryRequest request)
		{
			return new Category
			{
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now,
				CreatedBy = request.UserId,
				UpdatedBy = request.UserId,
				Name = request.Name,
				Description = request.Description,

			};
		}
	}
}
