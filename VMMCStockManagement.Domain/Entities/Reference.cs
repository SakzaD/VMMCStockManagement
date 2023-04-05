using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Models.Requests;

namespace VMMCStockManagement.Domain.Entities
{
	public class Reference : BaseEntity
	{
		public string Number { get; set; }

		public static Reference Create(ReferenceRequest request)
		{
			return new Reference
			{
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now,
				CreatedBy = request.UserId,
				UpdatedBy = request.UserId,
				Number = request.Number
			};
		}
	}
}
