using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Entities
{
	public class Province : BaseEntity
	{
		public string Name { get; set; }
		public long? CountryId { get; set; }
		public Country? Country { get; set; }

		internal static Province Create(string name, long countryId, string userId)
		{
			return new Province
			{
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now,
				CreatedBy = userId,
				UpdatedBy = userId,
				CountryId = countryId,
				Name = name,
			};
		}
	}
}
