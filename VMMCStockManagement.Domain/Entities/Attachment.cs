using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Enums;
using VMMCStockManagement.Domain.Models.Requests;

namespace VMMCStockManagement.Domain.Entities
{
	public class Attachment : BaseEntity
	{
		public string FileName { get; set; }
		public string OriginalFileName { get; set; }
		public AttachmentType AttachmentType { get; set; }

		public static Attachment Create(AttachmentRequest request)
		{
			return new Attachment
			{
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now,
				CreatedBy = request.UserId,
				UpdatedBy = request.UserId,
				FileName = request.FileName,
				OriginalFileName = request.OriginalFileName,
				AttachmentType = request.AttachmentType
			};
		}
	}
}
