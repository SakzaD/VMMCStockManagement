using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Interfaces.Files
{
	public interface IFileService
	{
		List<FileInfo> ReadFiles(string directory);
		Task<string> SaveFileAsync(IFormFile formFile, string folder);
		Task<string> SaveFileAsync(string base64Image, string folder);
		string SaveFile(string base64Image, string folder);
	}
}
