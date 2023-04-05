using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Enums;
using VMMCStockManagement.Domain.Interfaces.Repositories;
using VMMCStockManagement.Domain.Interfaces.Services.QueryServices;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.Model;
using VMMCStockManagement.Domain.Models.Responses;

namespace VMMCStockManagement.Domain.Services.QueryServices
{
	public class ModelQueryService : BaseQueryService<ModelFilter, Model>, IModelQueryService
	{

		public ModelQueryService(IQueryRepository<Model, ModelFilter> queryRepository,
			ILogger<BaseService> logger) : base(queryRepository, logger)
		{

		}

		public override string ServiceName => nameof(ModelQueryService);

		public ObjectListResponse<ModelListResponse> Filter(ModelFilter filter)
		{
			var response = new ObjectListResponse<ModelListResponse>();
			var data = queryRepository.Filter(filter).ToList();

			var mappedData = new List<ModelListResponse>();

			foreach (var item in data)
			{
				mappedData.Add(new ModelListResponse
				{
					Id = item.Id,
					Name = item.Name,
					MakeId = item.MakeId,
					Make = item?.Make?.Name
				});
			}
			response.Data = mappedData;
			response.CodeStatus = ResponseStatus.Success;

			return response;
		}

		public ObjectResponse<ModelResponse> Get(long id)
		{
			var response = new ObjectResponse<ModelResponse>();
			var model = queryRepository.GetById(id);

			var modelResponse = new ModelResponse
			{
				Id = model.Id,
				Name = model.Name,
				Make = model?.Make?.Name
			};

			response.Data = modelResponse;
			response.CodeStatus = ResponseStatus.Success;

			return response;
		}
	}
}
