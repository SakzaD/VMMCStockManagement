using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Models.Requests;

namespace VMMCStockManagement.Domain.Entities
{
	public class Model : BaseEntity
	{
		public string Name { get; set; }
		public long? MakeId { get; set; }
		public Make? Make { get; set; }

		public static Model Create(ModelRequest request)
		{
			return new Model
			{
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now,
				CreatedBy = request.UserId,
				UpdatedBy = request.UserId,
				Name = request.Name,
				MakeId = request.MakeId
			};
		}
	}
}
