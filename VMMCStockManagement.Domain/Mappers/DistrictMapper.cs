using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.District;

namespace VMMCStockManagement.Domain.Mappers
{
	public static class DistrictMapper
	{

		public static DistrictResponse Map(this District entity)
		{
			throw new NotImplementedException();
		}

		public static List<DistrictListResponse> Map(this List<District> entities)
		{
			var mapped = new List<DistrictListResponse>();

			foreach (var item in entities)
			{
				mapped.Add(new DistrictListResponse
				{
					//Id = item.Id,
					//Name = item.Name,
					//ProvinceId = item.ProvinceId.Value,
					//CountryId = item.Province.CountryId.Value

				});
			}

			return mapped;
		}

	}
}
