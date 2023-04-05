using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.Country;

namespace VMMCStockManagement.Domain.Mappers
{
	public static class CountryMapper
	{
		public static CountryResponse Map(this Country entity)
		{
			return new CountryResponse
			{

			};
		}

		public static List<CountryListResponse> Map(this List<Country> entities)
		{
			var mapped = new List<CountryListResponse>();

			foreach (var item in entities)
			{
				mapped.Add(new CountryListResponse
				{
					Id = item.Id,
					Name = item.Name

				});
			}

			return mapped;
		}
	}
}
