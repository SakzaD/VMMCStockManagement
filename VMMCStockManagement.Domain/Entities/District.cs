using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Entities
{
	public class District : BaseEntity
	{
		public string Name { get; set; }
		public long? ProvinceId { get; set; }
		public Province? Province { get; set; }

		public static District Create(string name, long provinceId, string userId)
		{
			return new District
			{
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now,
				CreatedBy = userId,
				UpdatedBy = userId,
				ProvinceId = provinceId,
				Name = name,
			};
		}
	}
}
