using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Interfaces.Repositories;
using VMMCStockManagement.Domain.Interfaces.Services.QueryServices;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse;

namespace VMMCStockManagement.Domain.Services.QueryServices
{
	public abstract class BaseQueryService<Fil, Ent> : BaseService, IBaseQueryService<Fil, Ent>
		where Fil : BaseFilter where Ent : BaseEntity
	{
		protected IQueryRepository<Ent, Fil> queryRepository;
		protected BaseQueryService(IQueryRepository<Ent, Fil> queryRepository,
			ILogger<BaseService> logger) : base(logger)
		{
			this.queryRepository = queryRepository;
		}

		public RawObjectListResponse<Ent> Filter(Fil filter)
		{
			var response = new RawObjectListResponse<Ent>();

			try
			{
				var results = queryRepository.Filter(filter);

				response.Data = results;
				response.CodeStatus = Enums.ResponseStatus.Success;
			}
			catch (Exception ex)
			{
				LogError(response, ex, "Error occurred when filtering data.");
			}
			return response;
		}

		public RawObjectResponse<Ent> GetById(long id)
		{
			var response = new RawObjectResponse<Ent>();

			try
			{
				var results = queryRepository.GetById(id);

				response.Data = results;
				response.CodeStatus = Enums.ResponseStatus.Success;
			}
			catch (Exception ex)
			{
				LogError(response, ex, "Error occurred reading data.");
			}
			return response;
		}
	}
}
