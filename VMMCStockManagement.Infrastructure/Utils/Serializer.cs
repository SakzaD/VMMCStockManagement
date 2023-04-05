using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Utils;

namespace VMMCStockManagement.Infrastructure.Utils
{
	public class Serializer : ISerializer
	{
		public string Serialize(object data)
		{
			var results = JsonSerializer.Serialize(data, new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase
			});

			return results;
		}

		public T? Deserialize<T>(string data)
		{
			var result = JsonSerializer.Deserialize<T>(data, new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase
			});

			return result;
		}

	}
}
