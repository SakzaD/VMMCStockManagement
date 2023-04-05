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
using VMMCStockManagement.Domain.Models.Responses.QueryResponse;
using VMMCStockManagement.Domain.Models.Responses;

namespace VMMCStockManagement.Domain.Services.QueryServices
{
	public class TicketQueryService : BaseQueryService<TicketFilter, Ticket>, ITicketQueryService
	{

		public TicketQueryService(IQueryRepository<Ticket, TicketFilter> queryRepository,
			ILogger<BaseService> logger) : base(queryRepository, logger)
		{

		}

		public override string ServiceName => nameof(TicketQueryService);

		public ObjectListResponse<TicketResponse> Filter(TicketFilter filter)
		{
			var response = new ObjectListResponse<TicketResponse>();
			var data = queryRepository.Filter(filter).ToList();

			var mappedData = new List<TicketResponse>();

			foreach (var item in data)
			{
				mappedData.Add(new TicketResponse
				{
					Id = item.Id,
					Number = item.Number
				});
			}
			response.Data = mappedData;
			response.CodeStatus = ResponseStatus.Success;

			return response;
		}

		public ObjectResponse<TicketResponse> Get(long id)
		{
			throw new NotImplementedException();
		}
	}
}
