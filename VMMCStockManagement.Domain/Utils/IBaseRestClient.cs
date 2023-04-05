using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Utils
{
	public interface IBaseRestClient
	{
		Task<T?> Get<T>(string url, Dictionary<string, string> headers);
		Task Post(string url, object data, Dictionary<string, string> headers);
		Task<T?> Post<T>(string url, object data, Dictionary<string, string> headers);
	}
}
