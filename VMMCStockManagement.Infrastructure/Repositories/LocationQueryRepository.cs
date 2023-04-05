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
	public class LocationQueryRepository : QueryRepository<Location, LocationFilter>,
		IQueryRepository<Location, LocationFilter>
	{
		public LocationQueryRepository(VMMCStockManagementDbContext context) : base(context)
		{
		}
		protected override Location GetByAny(long id)
		{
			throw new NotImplementedException();
		}
		protected override IQueryable<Location> FilterData(LocationFilter filter)
		{
			var data = context.Set<Location>()
			 .Where(x => x.Status == EntityStatus.Active)
			 .AsQueryable();

			//if (filter.SubDistrictId != null)
			//{
			//    data = data.Where(x => x.SubDistrictId == filter.SubDistrictId);
			//}

			data = SpecifyInclude(data);

			return data;
		}

		protected override Location Get(long id)
		{
			var data = context.Set<Location>()
			.FirstOrDefault(x => x.Id == id);

			return data;
		}

		protected override IQueryable<Location> SpecifyInclude(IQueryable<Location> query)
		{
			//query = query

			//    .Include(x => x.SubDistrict);

			return query;
		}
	}
}
