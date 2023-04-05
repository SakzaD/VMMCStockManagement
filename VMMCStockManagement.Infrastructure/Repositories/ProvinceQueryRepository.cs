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
	public class ProvinceQueryRepository : QueryRepository<Province, ProvinceFilter>,
		 IQueryRepository<Province, ProvinceFilter>
	{
		public ProvinceQueryRepository(VMMCStockManagementDbContext context) : base(context)
		{

		}
		protected override Province GetByAny(long id)
		{
			throw new NotImplementedException();
		}
		protected override IQueryable<Province> FilterData(ProvinceFilter filter)
		{
			var data = context.Set<Province>()
			  .Where(x => x.Status == EntityStatus.Active)
			  .AsQueryable();


			if (filter.CountryId != null)
				data = data.Where(x => x.CountryId == filter.CountryId);


			return data;
		}

		protected override Province Get(long id)
		{
			var entity = context.Set<Province>()
			  .AsNoTracking()
			  .FirstOrDefault(x => x.Id == id && x.Status == EntityStatus.Active);

			return entity;
		}
	}
}
