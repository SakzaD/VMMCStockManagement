using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VMMCStockManagement.Domain.Interfaces.Services.CommandServices;
using VMMCStockManagement.Domain.Models.Requests;

namespace VMMCStockManagement.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CountryController : ControllerBase
	{
		private readonly ICountryCommandService countryCommandService;
		public CountryController(ICountryCommandService countryCommandService)
		{
			this.countryCommandService = countryCommandService;
		}

		[HttpPost("create")]
		public IActionResult Create([FromBody] CountryRequest request)
		{
			var res = countryCommandService.Add(request);
			return Ok(res);
		}


		[HttpPost("delete")]
		public IActionResult Delete([FromBody] BaseRequest request)
		{
			var res = countryCommandService.Delete(request);
			return Ok(res);
		}


		[HttpPost("update")]
		public IActionResult Update([FromBody] CountryRequest request)
		{
			var res = countryCommandService.Edit(request);
			return Ok(res);
		}
	}
}
