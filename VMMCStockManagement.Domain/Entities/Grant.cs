using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Models.Requests;

namespace VMMCStockManagement.Domain.Entities
{
	public class Grant : BaseEntity
	{
        public string Name { get; set; }
		public long? CountryId { get; set; }
		public Country? Country { get; set; }

		public static Grant Create(GrantRequest request)
		{
			return new Grant
			{
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now,
				CreatedBy = request.UserId,
				UpdatedBy = request.UserId,
				Name = request.Name,
				CountryId = request.CountryId
			};
		}

	}
}
