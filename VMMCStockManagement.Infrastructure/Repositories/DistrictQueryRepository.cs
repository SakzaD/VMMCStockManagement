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
	public class DistrictQueryRepository : QueryRepository<District, DistrictFilter>,
	   IQueryRepository<District, DistrictFilter>
	{
		public DistrictQueryRepository(VMMCStockManagementDbContext context) : base(context)
		{

		}
		protected override District GetByAny(long id)
		{
			throw new NotImplementedException();
		}
		protected override IQueryable<District> FilterData(DistrictFilter filter)
		{
			var data = context.Set<District>()
			  .Where(x => x.Status == EntityStatus.Active)
			  .AsQueryable();

			if (filter.ProvinceId != null)
				data = data.Where(x => x.ProvinceId == filter.ProvinceId);

			return data;
		}

		protected override District Get(long id)
		{
			var entity = context.Set<District>()
			  .AsNoTracking()
			  .FirstOrDefault(x => x.Id == id && x.Status == EntityStatus.Active);

			return entity;
		}

		protected override IQueryable<District> SpecifyInclude(IQueryable<District> query)
		{
			query = query;

			//.Include(x => x.Province)
			//.ThenInclude(x => x.Country);

			return query;
		}
	}
}
