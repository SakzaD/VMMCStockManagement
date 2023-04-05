using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VMMCStockManagement.Domain.Interfaces.Services.CommandServices;
using VMMCStockManagement.Domain.Models.Requests;

namespace VMMCStockManagement.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TicketController : ControllerBase
	{
		private readonly ITicketCommandService ticketCommandService;
		public TicketController(ITicketCommandService ticketCommandService)
		{
			this.ticketCommandService = ticketCommandService;
		}

		[HttpPost("generate")]
		public IActionResult CreateAsset([FromBody] TicketRequest request)
		{
			var res = ticketCommandService.GenerateTicket(request);
			return Ok(res);
		}
	}
}
