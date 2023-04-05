using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.Facility;

namespace VMMCStockManagement.Domain.Mappers
{
	public static class FacilityMapper
	{
		public static FacilityResponse Map(this Facility entity)
		{
			throw new NotImplementedException();
		}

		public static List<FacilityListResponse> Map(this List<Facility> entities)
		{
			var mapped = new List<FacilityListResponse>();

			foreach (var item in entities)
			{
				mapped.Add(new FacilityListResponse
				{
					Id = item.Id,
					Name = item.Name,
					//SubDistrictId = item.SubDistrictId.Value,
					//DistrictId = item.SubDistrict.DistrictId.Value,
					//ProvinceId = item.SubDistrict.District.ProvinceId.Value,
					//CountryId = item.SubDistrict.District.Province.CountryId.Value,
				});
			}

			return mapped;
		}
	}
}
