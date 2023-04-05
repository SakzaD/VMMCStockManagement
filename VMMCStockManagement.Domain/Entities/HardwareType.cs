using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Models.Requests;

namespace VMMCStockManagement.Domain.Entities
{
	public class HardwareType : BaseEntity
	{
		public string Name { get; set; }

		internal static HardwareType Create(HardwareTypeRequest request)
		{
			throw new NotImplementedException();
		}
	}
}
