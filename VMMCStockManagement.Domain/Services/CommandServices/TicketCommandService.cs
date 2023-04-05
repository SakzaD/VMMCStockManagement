using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Interfaces.Repositories;
using VMMCStockManagement.Domain.Interfaces.Services.CommandServices;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Requests;
using VMMCStockManagement.Domain.Models.Responses;

namespace VMMCStockManagement.Domain.Services.CommandServices
{
	public class TicketCommandService : BaseCommandService<Ticket>, ITicketCommandService
	{

		private readonly IQueryRepository<Ticket, TicketFilter> queryRepository;
		public TicketCommandService(ICommandRepository<Ticket> commandRepository,
			ILogger<BaseCommandService<Ticket>> logger,
			IQueryRepository<Ticket, TicketFilter> queryRepository) : base(commandRepository, logger)
		{
			this.queryRepository = queryRepository;
		}

		public override string ServiceName => nameof(TicketCommandService);

		public BaseResponse GenerateTicket(TicketRequest request)
		{
			long number = 100000;

			var ticket = queryRepository.GetAll().OrderBy(x => x.Id).LastOrDefault();

			if (ticket != null)
			{
				number = Convert.ToInt64(ticket.Number) + 1;
			}

			request.Number = number.ToString();

			ticket = Ticket.Create(request);
			return Create(ticket);
		}

		public BaseResponse Add(TicketRequest request)
		{
			Ticket location = Ticket.Create(request);

			return Create(location);
		}

		public override void AfterCreation(Ticket entity)
		{

		}

		public BaseResponse Delete(BaseRequest request)
		{
			var district = queryRepository.GetById(request.Id.Value);

			if (district == null)
			{
				return new BaseResponse
				{
					CodeStatus = Enums.ResponseStatus.Fail,
					Message = "Invalid asset selected"
				};
			}


			district.UpdatedAt = DateTime.Now;
			district.UpdatedBy = request.UserId;
			district.Status = Enums.EntityStatus.Deleted;
			var response = Delete(district);

			return response;
		}

		public BaseResponse Edit(TicketRequest request)
		{
			var district = queryRepository.GetById(request.Id.Value);

			if (district == null)
				return new BaseResponse
				{
					CodeStatus = Enums.ResponseStatus.Fail,
					Message = "Invalid asset selected"
				};


			district.UpdatedAt = DateTime.Now;

			var response = Update(district);

			return response;
		}


	}
}
