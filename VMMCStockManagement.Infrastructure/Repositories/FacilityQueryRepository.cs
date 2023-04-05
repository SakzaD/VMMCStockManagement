using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Enums;
using VMMCStockManagement.Domain.Interfaces.Repositories;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Infrastructure.DbContexts;
using VMMCStockManagement.Infrastructure.Repositories.BaseRepositories;

namespace VMMCStockManagement.Infrastructure.Repositories
{
	public class FacilityQueryRepository : QueryRepository<Facility, FacilityFilter>,
	  IQueryRepository<Facility, FacilityFilter>
	{
		public FacilityQueryRepository(VMMCStockManagementDbContext context) : base(context)
		{

		}
		protected override Facility GetByAny(long id)
		{
			throw new NotImplementedException();
		}
		protected override IQueryable<Facility> FilterData(FacilityFilter filter)
		{
			var data = context.Set<Facility>()
			  .Where(x => x.Status == EntityStatus.Active)
			  .AsQueryable();

			data = SpecifyInclude(data);

			if (filter.SubDistrictId != null)
				data = data.Where(x => x.SubDistrictId == filter.SubDistrictId);

			return data;
		}

		protected override Facility Get(long id)
		{
			var entity = context.Set<Facility>()
			  .AsNoTracking()
			  .FirstOrDefault(x => x.Id == id && x.Status == EntityStatus.Active);

			return entity;
		}

		protected override IQueryable<Facility> SpecifyInclude(IQueryable<Facility> query)
		{
			query = query
				.Include(x => x.SubDistrict)
				.ThenInclude(x => x.District)
				.ThenInclude(x => x.Province)
				.ThenInclude(x => x.Country);

			return query;
		}
	}
}
