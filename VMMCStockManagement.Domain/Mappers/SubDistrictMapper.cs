using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.SubDistrict;

namespace VMMCStockManagement.Domain.Mappers
{
	public static class SubDistrictMapper
	{
		public static SubDistrictResponse Map(this SubDistrict entity)
		{
			throw new NotImplementedException();
		}

		public static List<SubDistrictListResponse> Map(this List<SubDistrict> entities)
		{
			var mapped = new List<SubDistrictListResponse>();

			foreach (var item in entities)
			{
				mapped.Add(new SubDistrictListResponse
				{
					//Id = item.Id,
					//Name = item.Name,
					//DistrictId = item.DistrictId.Value,
					//ProvinceId = item.District.ProvinceId.Value,
					//CountryId = item.District.Province.CountryId.Value,
				});
			}

			return mapped;
		}
	}
}
