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
	public class MakeQueryRepository : QueryRepository<Make, MakeFilter>,
		IQueryRepository<Make, MakeFilter>
	{
		public MakeQueryRepository(VMMCStockManagementDbContext context) : base(context)
		{
		}
		protected override Make GetByAny(long id)
		{
			throw new NotImplementedException();
		}
		protected override IQueryable<Make> FilterData(MakeFilter filter)
		{
			var data = context.Set<Make>()
			 .Where(x => x.Status == EntityStatus.Active)
			 .AsQueryable();

			if (filter.Name != null)
			{
				data = data.Where(x => x.Name == filter.Name);
			}

			data = SpecifyInclude(data);

			return data;
		}

		protected override Make Get(long id)
		{
			var data = context.Set<Make>()
			.FirstOrDefault(x => x.Id == id);

			return data;
		}

		protected override IQueryable<Make> SpecifyInclude(IQueryable<Make> query)
		{
			//query = query

			//    .Include(x => x.Model);

			return query;
		}
	}
}
