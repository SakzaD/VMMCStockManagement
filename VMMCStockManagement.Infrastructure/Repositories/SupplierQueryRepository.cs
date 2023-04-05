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
	public class SupplierQueryRepository : QueryRepository<Supplier, SupplierFilter>,
	  IQueryRepository<Supplier, SupplierFilter>
	{
		public SupplierQueryRepository(VMMCStockManagementDbContext context) : base(context)
		{

		}
		protected override Supplier GetByAny(long id)
		{
			throw new NotImplementedException();
		}
		protected override IQueryable<Supplier> FilterData(SupplierFilter filter)
		{
			var data = context.Set<Supplier>()
			  .Where(x => x.Status == EntityStatus.Active)
			  .AsQueryable();

			data = SpecifyInclude(data);

			if (filter.Name != null)
				data = data.Where(x => x.Name == filter.Name);

			return data;
		}

		protected override Supplier Get(long id)
		{
			var entity = context.Set<Supplier>()
			  .AsNoTracking()
			  .FirstOrDefault(x => x.Id == id && x.Status == EntityStatus.Active);

			return entity;
		}

		protected override IQueryable<Supplier> SpecifyInclude(IQueryable<Supplier> query)
		{
			query = query;			

			return query;
		}
	}
}
