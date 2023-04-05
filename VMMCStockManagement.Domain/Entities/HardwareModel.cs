using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Models.Requests;

namespace VMMCStockManagement.Domain.Entities
{
	public class HardwareModel : BaseEntity
	{
		public string Name { get; set; }

		internal static HardwareModel Create(HardwareModelRequest request)
		{
			throw new NotImplementedException();
		}
	}
}
