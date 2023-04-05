using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Enums;

namespace VMMCStockManagement.Domain.Entities
{
	public class BaseEntity
	{
        public long Id { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public string CreatedBy { get; set; }
		public string UpdatedBy { get; set; }
		public EntityStatus Status { get; set; } = EntityStatus.Active;

	}
}
