using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Enums;

namespace VMMCStockManagement.Domain.Models.Requests
{
	public class AttachmentRequest : BaseRequest
	{
		public string FileName { get; set; }
		public string OriginalFileName { get; set; }
		public AttachmentType AttachmentType { get; set; }
	}
}
