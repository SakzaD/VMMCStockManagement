using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Models;

namespace VMMCStockManagement.Domain.Utils
{
	public interface IBulkUploadService
	{
		Task<string> SaveFileAsync(IFormFile formFile);
		FileInfo SaveFile(IFormFile formFile);
		List<BulkStockModel> ReadFile(FileInfo fileInfo);
		Task<List<BulkStockModel>> ReadFileAsync(FileInfo fileInfo);
	}
}
