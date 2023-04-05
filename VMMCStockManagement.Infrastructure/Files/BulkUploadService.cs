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
	public class BulkUploadService : IBulkUploadService
	{
		private readonly IConfiguration configuration;
		private readonly ILogger<BulkUploadService> logger;
		public BulkUploadService(IConfiguration configuration, ILogger<BulkUploadService> logger)
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
			var folder = configuration.GetValue<string>("SystemConfig:UploadDirectory:Asset");

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

		public List<BulkStockModel> ReadFile(FileInfo fileInfo)
		{
			var rawData = new List<BulkStockModel>();
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

					var rowData = new BulkStockModel
					{
						RowNumber = i,
						RegisteredNumber = workSheet.Cells[i, 2].Text.ToString().Trim(),
						Make = workSheet.Cells[i, 3].Text.ToString().Trim(),
						Model = workSheet.Cells[i, 4].Text.ToString().Trim(),
						Description = workSheet.Cells[i, 5].Text.ToString().Trim(),
						SerialNumber = workSheet.Cells[i, 6].Text.ToString().Trim(),
						UserName = workSheet.Cells[i, 7].Text.ToString().Trim(),
						Grant = workSheet.Cells[i, 8].Text.ToString().Trim(),
						Department = workSheet.Cells[i, 9].Text.ToString().Trim(),
						Location = workSheet.Cells[i, 10].Text.ToString().Trim(),
						PurchaseOrReceivedDate = workSheet.Cells[i, 11].Text.ToString().Trim(),
						PurchasePrice = workSheet.Cells[i, 12].Text.ToString().Trim(),
						SupplierName = workSheet.Cells[i, 13].Text.ToString().Trim(),
						WarrantyType = workSheet.Cells[i, 14].Text.ToString().Trim(),
						TransferDate = workSheet.Cells[i, 15].Text.ToString().Trim(),
						ReferenceNumber = workSheet.Cells[i, 16].Text.ToString().Trim(),
					};
					rawData.Add(rowData);
				}
			}

			return rawData;
		}

		public async Task<List<BulkStockModel>> ReadFileAsync(FileInfo fileInfo)
		{
			throw new NotImplementedException();
		}


	}
}
