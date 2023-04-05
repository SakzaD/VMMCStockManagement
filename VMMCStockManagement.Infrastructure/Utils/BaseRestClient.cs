using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Utils;

namespace VMMCStockManagement.Infrastructure.Utils
{
	public class BaseRestClient : IBaseRestClient
	{
		private readonly ISerializer serializer;
		private readonly ILogger<BaseRestClient> logger;
		public BaseRestClient(ISerializer serializer, ILogger<BaseRestClient> logger)
		{
			this.serializer = serializer;
			this.logger = logger;
		}

		public async Task<T?> Get<T>(string url, Dictionary<string, string> headers)
		{
			try
			{
				using (HttpClient client = new HttpClient())
				{
					client.DefaultRequestHeaders.Accept.Clear();
					client.DefaultRequestHeaders.Accept.Add(
						new MediaTypeWithQualityHeaderValue("application/json"));

					var response = await client.GetAsync(url);
					var content = await response.Content.ReadAsStringAsync();

					return serializer.Deserialize<T>(content);
				}
			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message);
			}

			return default;
		}

		public async Task Post(string url, object data, Dictionary<string, string> headers)
		{
			try
			{
				using (HttpClient client = new HttpClient())
				{
					if (headers != null)
					{
						foreach (var header in headers)
						{
							client.DefaultRequestHeaders.Add(header.Key, header.Value);
						}
					}

					var response = await client.GetAsync(url);
					var content = await response.Content.ReadAsStringAsync();
				}
			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message);
			}
		}

		public async Task<T?> Post<T>(string url, object data, Dictionary<string, string> headers)
		{
			try
			{
				using (HttpClient client = new HttpClient())
				{
					if (headers != null)
					{
						foreach (var header in headers)
						{
							client.DefaultRequestHeaders.Add(header.Key, header.Value);
						}
					}
					var json = serializer.Serialize(data);
					var strContent = new StringContent(json, Encoding.UTF8, "application/json");
					var response = await client.PostAsync(url, strContent);
					var content = await response.Content.ReadAsStringAsync();

					return serializer.Deserialize<T>(content);
				}
			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message);
			}

			return default;
		}
	}
}
