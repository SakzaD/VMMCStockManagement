using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Models.Requests;
using VMMCStockManagement.Domain.Models.Responses;

namespace VMMCStockManagement.Domain.Interfaces.Services.CommandServices
{
	public interface ITicketCommandService : IBaseCommandService<Ticket>
	{
		BaseResponse GenerateTicket(TicketRequest request);
		BaseResponse Edit(TicketRequest request);
		BaseResponse Delete(BaseRequest request);
	}
}
