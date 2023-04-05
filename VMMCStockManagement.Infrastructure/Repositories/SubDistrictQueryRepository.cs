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
	public class SubDistrictQueryRepository : QueryRepository<SubDistrict, SubDistrictFilter>,
		 IQueryRepository<SubDistrict, SubDistrictFilter>
	{
		public SubDistrictQueryRepository(VMMCStockManagementDbContext context) : base(context)
		{

		}
		protected override SubDistrict GetByAny(long id)
		{
			throw new NotImplementedException();
		}
		protected override IQueryable<SubDistrict> FilterData(SubDistrictFilter filter)
		{
			var data = context.Set<SubDistrict>()
			  .Where(x => x.Status == EntityStatus.Active)
			  .AsQueryable();

			if (filter.DistrictId != null)
				data = data.Where(x => x.DistrictId == filter.DistrictId);

			data = SpecifyInclude(data);

			return data;
		}

		protected override SubDistrict Get(long id)
		{
			var entity = context.Set<SubDistrict>()
			  .AsNoTracking()
			  .FirstOrDefault(x => x.Id == id && x.Status == EntityStatus.Active);

			return entity;
		}

		protected override IQueryable<SubDistrict> SpecifyInclude(IQueryable<SubDistrict> query)
		{
			query = query
				.Include(x => x.District)
				.ThenInclude(x => x.Province)
				.ThenInclude(x => x.Country);

			return query;
		}
	}
}
