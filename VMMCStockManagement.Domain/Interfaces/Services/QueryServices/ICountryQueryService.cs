using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.Country;
using VMMCStockManagement.Domain.Models.Responses;

namespace VMMCStockManagement.Domain.Interfaces.Services.QueryServices
{
	public interface ICountryQueryService : IBaseQueryService<CountryFilter, Country>
	{
		ObjectListResponse<CountryListResponse> Filter(CountryFilter filter);
		ObjectResponse<CountryResponse> Get(long id);
	}
}
