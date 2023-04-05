using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Requests
{
	public class FileUploadRequest : BaseRequest
	{
		public IFormFile AssetsFile { get; set; }
	}
}
