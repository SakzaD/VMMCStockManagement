using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.Province;

namespace VMMCStockManagement.Domain.Mappers
{
	public static class ProvinceMapper
	{
		public static ProvinceResponse Map(this Province entity)
		{
			throw new NotImplementedException();
		}

		public static List<ProvinceListResponse> Map(this List<Province> entities)
		{
			var mapped = new List<ProvinceListResponse>();

			foreach (var item in entities)
			{
				mapped.Add(new ProvinceListResponse
				{
					Id = item.Id,
					Name = item.Name,
					CountryId = item.CountryId.Value

				});
			}

			return mapped;
		}
	}
}
