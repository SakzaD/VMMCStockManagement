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
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.Supplier;
using VMMCStockManagement.Domain.Models.Responses;

namespace VMMCStockManagement.Domain.Services.QueryServices
{
	public class SupplierQueryService : BaseQueryService<SupplierFilter, Supplier>, ISupplierQueryService
	{

		public SupplierQueryService(IQueryRepository<Supplier, SupplierFilter> queryRepository,
			ILogger<BaseService> logger) : base(queryRepository, logger)
		{

		}

		public override string ServiceName => nameof(SupplierQueryService);

		public ObjectListResponse<SupplierListResponse> Filter(SupplierFilter filter)
		{
			var response = new ObjectListResponse<SupplierListResponse>();
			var data = queryRepository.Filter(filter).ToList();

			var mappedData = new List<SupplierListResponse>();
			foreach (var item in data)
			{
				mappedData.Add(new SupplierListResponse
				{
					Id = item.Id,
					Name = item.Name,
					Description = item.Description,
				});

			}
			response.Data = mappedData;
			response.CodeStatus = ResponseStatus.Success;

			return response;
		}

		public ObjectResponse<SupplierResponse> Get(long id)
		{
			var response = new ObjectResponse<SupplierResponse>();
			var data = queryRepository.GetById(id);
			var mappedData = new SupplierResponse
			{
				Id = data.Id,
				Name = data.Name,
				Description = data.Description,
			};

			response.Data = mappedData;
			response.CodeStatus = ResponseStatus.Success;

			return response;
		}
	}
}
