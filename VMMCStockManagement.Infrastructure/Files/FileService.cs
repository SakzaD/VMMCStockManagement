using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Interfaces.Files;

namespace VMMCStockManagement.Infrastructure.Files
{
	public class FileService : IFileService
	{
		private readonly IConfiguration configuration;
		private readonly ILogger<FileService> logger;

		public FileService(IConfiguration configuration, ILogger<FileService> logger)
		{
			this.configuration = configuration;
			this.logger = logger;
		}


		public string FileDirectory { get; set; }

		private string GetFilePath(string folder)
		{
			var uploadDirectory = configuration.GetValue<string>("SystemConfig:UploadDirectory:MainDirectory");
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

		public string SaveFile(string base64Image, string folder)
		{
			string path = GetFilePath(folder);

			var timeStamp = DateTime.Now.ToString("yyyyMMddHHmmssffff");
			try
			{

				string actualImg = base64Image.Replace("data:image/jpeg;base64,", "");

				byte[] _rgbBytes = Convert.FromBase64String(actualImg);
				var ext = ".jpg";

				string fileName = string.Format("{0}{1}{2}", timeStamp, Guid.NewGuid().ToString(), ext);

				string filePath = Path.Combine(path, fileName);

				using (FileStream imageFile = new FileStream(filePath, FileMode.Create))
				{
					imageFile.WriteAsync(_rgbBytes, 0, _rgbBytes.Length);
					imageFile.FlushAsync();
				}

				return fileName;
			}
			catch (Exception ex)
			{

			}

			return string.Empty;

		}

		public async Task<string> SaveFileAsync(string base64Image, string folder)
		{
			string path = GetFilePath(folder);

			var timeStamp = DateTime.Now.ToString("yyyyMMddHHmmssffff");
			try
			{

				string actualImg = base64Image.Replace("data:image/jpeg;base64,", "");

				byte[] _rgbBytes = Convert.FromBase64String(actualImg);
				var ext = ".jpg";

				string fileName = string.Format("{0}{1}{2}", timeStamp, Guid.NewGuid().ToString(), ext);

				string filePath = Path.Combine(path, fileName);

				using (FileStream imageFile = new FileStream(filePath, FileMode.Create))
				{
					await imageFile.WriteAsync(_rgbBytes, 0, _rgbBytes.Length);
					await imageFile.FlushAsync();
				}

				return fileName;
			}
			catch (Exception ex)
			{

			}

			return string.Empty;

		}

		public async Task<string> SaveFileAsync(IFormFile formFile, string folder)
		{
			string path = GetFilePath(folder);

			if (string.IsNullOrEmpty(path))
				return string.Empty;

			var timeStamp = DateTime.Now.ToString("yyyyMMddHHmmssffff");

			string fileNameWithoutExtension = formFile.FileName.Replace(' ', '_').Replace('-', '_').ToLower();
			var ext = Path.GetExtension(fileNameWithoutExtension);

			if (fileNameWithoutExtension.Contains(ext))
			{
				fileNameWithoutExtension = fileNameWithoutExtension[..fileNameWithoutExtension.LastIndexOf(ext)];
			}

			string fileName = string.Format("{0}_{1}_{2}", fileNameWithoutExtension, timeStamp, ext);

			string filePath = Path.Combine(path, fileName);

			try
			{

				if (formFile.Length > 0)
				{

					using (Stream fileStream = new FileStream(filePath, FileMode.Create))
					{
						await formFile.CopyToAsync(fileStream);
					}
				}
			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message);
			}

			return fileName;
		}

		public List<FileInfo> ReadFiles(string directory)
		{
			try
			{
				return new DirectoryInfo(directory)
				.GetFiles("*.*", SearchOption.AllDirectories).ToList();
			}
			catch (Exception ex)
			{
				logger.LogError("Directory: " + directory);
				logger.LogError("1023: " + ex.Message);
				return new List<FileInfo>();
			}
		}

	}
}
