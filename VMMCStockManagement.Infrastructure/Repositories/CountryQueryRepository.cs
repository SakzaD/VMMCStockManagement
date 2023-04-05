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
	public class CountryQueryRepository : QueryRepository<Country, CountryFilter>,
		IQueryRepository<Country, CountryFilter>
	{
		public CountryQueryRepository(VMMCStockManagementDbContext context) : base(context)
		{

		}
		protected override Country GetByAny(long id)
		{
			throw new NotImplementedException();
		}
		protected override IQueryable<Country> FilterData(CountryFilter filter)
		{
			var data = context.Set<Country>()
			  .Where(x => x.Status == EntityStatus.Active)
			  .AsQueryable();

			return data;
		}

		protected override Country Get(long id)
		{
			var entity = context.Set<Country>()
			  .AsNoTracking()
			  .FirstOrDefault(x => x.Id == id && x.Status == EntityStatus.Active);

			return entity;
		}
	}
}
