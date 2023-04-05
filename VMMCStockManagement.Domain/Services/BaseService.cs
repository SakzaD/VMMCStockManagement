using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Models.Responses;

namespace VMMCStockManagement.Domain.Services
{
	public abstract class BaseService
	{
		protected ILogger<BaseService> logger;

		public BaseService(ILogger<BaseService> logger)
		{
			this.logger = logger;
		}

		public abstract string ServiceName { get; }

		protected void LogError(BaseResponse result, Exception ex, string message)
		{
			result.CodeStatus = Enums.ResponseStatus.Fail;
			result.Message = result.Message;
			this.logger.LogError(ex, ex.Message + $"{ServiceName}");
		}
	}
}
