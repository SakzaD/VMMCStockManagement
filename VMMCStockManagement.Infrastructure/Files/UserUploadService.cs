using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Models;
using VMMCStockManagement.Domain.Utils;

namespace VMMCStockManagement.Infrastructure.Files
{
	public class UserUploadService : IUserUploadService
	{
		private readonly IConfiguration configuration;
		private readonly ILogger<UserUploadService> logger;
		public UserUploadService(IConfiguration configuration, ILogger<UserUploadService> logger)
		{
			this.configuration = configuration;
			this.logger = logger;
		}

		public FileInfo SaveFile(IFormFile formFile)
		{
			throw new NotImplementedException();
		}


		private string GetFilePath()
		{
			var uploadDirectory = configuration.GetValue<string>("SystemConfig:UploadDirectory:MainDirectory");
			var folder = configuration.GetValue<string>("SystemConfig:UploadDirectory:User");

			var path = Path.Combine(uploadDirectory, folder);

			if (!Directory.Exists(path))
			{
				try
				{
					Directory.CreateDirectory(path);
				}
				catch (Exception ex)
				{
					logger.LogError(ex.Message);
				}
			}

			return path;
		}

		public async Task<string> SaveFileAsync(IFormFile formFile)
		{
			string path = GetFilePath();

			if (string.IsNullOrEmpty(path))
				return string.Empty;

			var timeStamp = DateTime.Now.ToString("yyyyMMddHHmmssffff");

			string fileNameWithoutExtension = formFile.FileName.Replace(' ', '_').Replace('-', '_').ToLower();
			var ext = Path.GetExtension(fileNameWithoutExtension);

			if (fileNameWithoutExtension.Contains(ext))
			{
				fileNameWithoutExtension = fileNameWithoutExtension[..fileNameWithoutExtension.LastIndexOf(ext)];
			}
			string finalFileName = Path.Combine(path, string.Format("{0}_{1}_{2}", fileNameWithoutExtension, timeStamp, ext));


			if (formFile.Length > 0)
			{

				using (Stream fileStream = new FileStream(finalFileName, FileMode.Create))
				{
					await formFile.CopyToAsync(fileStream);
				}
			}

			return finalFileName;
		}

		public List<UserModel> ReadFile(FileInfo fileInfo)
		{
			var rawData = new List<UserModel>();
			var rowNumberToStart = 11;

			int? definedRowNum = configuration.GetValue<int>("BulkUploadStartingRow");

			if (definedRowNum != null)
			{
				rowNumberToStart = definedRowNum.Value;
			}

			using (var package = new ExcelPackage(fileInfo))
			{
				var workBook = package.Workbook;

				var workSheet = workBook.Worksheets.First();

				int totalRows = workSheet.Dimension.End.Row;

				for (var i = 1; i <= totalRows; i++)
				{
					if (i < rowNumberToStart)
						continue;

					int columnNumber = 1;
					var id = workSheet.Cells[i, 1].Text.ToString();

					var rowData = new UserModel
					{
						RowNumber = i,
						Grant = workSheet.Cells[i, 1].Text.ToString(),
						Province = workSheet.Cells[i, 2].Text.ToString(),
						EmployeeCode = workSheet.Cells[i, 3].Text.ToString(),
						KnownAsName = workSheet.Cells[i, 4].Text.ToString(),
						FirstName = workSheet.Cells[i, 5].Text.ToString(),
						LastName = workSheet.Cells[i, 6].Text.ToString(),
						JobTitle = workSheet.Cells[i, 7].Text.ToString(),
						ContactNumber = workSheet.Cells[i, 8].Text.ToString(),
						Email = workSheet.Cells[i, 9].Text.ToString(),


					};
					rawData.Add(rowData);
				}
			}

			return rawData;
		}

		public async Task<List<UserModel>> ReadFileAsync(FileInfo fileInfo)
		{
			throw new NotImplementedException();
		}

		List<UserModel> IUserUploadService.ReadFile(FileInfo fileInfo)
		{
			throw new NotImplementedException();
		}

		Task<List<UserModel>> IUserUploadService.ReadFileAsync(FileInfo fileInfo)
		{
			throw new NotImplementedException();
		}
	}
}
