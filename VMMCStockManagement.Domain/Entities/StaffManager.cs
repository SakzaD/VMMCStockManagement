using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Models.Requests;

namespace VMMCStockManagement.Domain.Entities
{
	public class StaffManager : BaseEntity
	{
		public string UserId { get; set; }
		public string ManagerId { get; set; }

		public static StaffManager Create(StaffManagerRequest request)
		{
			return new StaffManager
			{
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now,
				CreatedBy = request.UserId,
				UpdatedBy = request.UserId,
				UserId = request.UserId,
				ManagerId = request.ManagerId
			};
		}
	}
}
