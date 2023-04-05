using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Requests
{
	public class UserUploadRequest : BaseRequest
	{
		public IFormFile UserFile { get; set; }
	}
}
