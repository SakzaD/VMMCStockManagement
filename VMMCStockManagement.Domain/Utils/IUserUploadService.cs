using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Models;

namespace VMMCStockManagement.Domain.Utils
{
	public interface IUserUploadService
	{
		Task<string> SaveFileAsync(IFormFile formFile);
		FileInfo SaveFile(IFormFile formFile);
		List<UserModel> ReadFile(FileInfo fileInfo);
		Task<List<UserModel>> ReadFileAsync(FileInfo fileInfo);
	}
}
