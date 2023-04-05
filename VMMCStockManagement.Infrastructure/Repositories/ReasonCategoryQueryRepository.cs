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
	public class ReasonCategoryQueryRepository : QueryRepository<ReasonCategory, ReasonCategoryFilter>,
		IQueryRepository<ReasonCategory, ReasonCategoryFilter>
	{
		public ReasonCategoryQueryRepository(VMMCStockManagementDbContext context) : base(context)
		{
		}
		protected override ReasonCategory GetByAny(long id)
		{
			throw new NotImplementedException();
		}
		protected override IQueryable<ReasonCategory> FilterData(ReasonCategoryFilter filter)
		{
			var data = context.Set<ReasonCategory>()
			 .Where(x => x.Status == EntityStatus.Active)

			 .AsQueryable();

			if (filter.Description != null)
			{
				data = data.Where(x => x.Description.Contains(filter.Description));
			}

			data = SpecifyInclude(data);

			return data;
		}
		protected override ReasonCategory Get(long id)
		{
			var data = context.Set<ReasonCategory>()

			.FirstOrDefault(x => x.Id == id);

			data = SpecifyInclude(data);
			return data;
		}
		protected override IQueryable<ReasonCategory> SpecifyInclude(IQueryable<ReasonCategory> query)
		{
			query = query;

			return query;
		}
	}
}
