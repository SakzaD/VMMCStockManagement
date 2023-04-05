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
	public class ReasonQueryRepository : QueryRepository<Reason, ReasonFilter>,
		IQueryRepository<Reason, ReasonFilter>
	{
		public ReasonQueryRepository(VMMCStockManagementDbContext context) : base(context)
		{
		}
		protected override Reason GetByAny(long id)
		{
			throw new NotImplementedException();
		}
		protected override IQueryable<Reason> FilterData(ReasonFilter filter)
		{
			var data = context.Set<Reason>()
			.Include(x => x.ReasonCategory)
			 .Where(x => x.Status == EntityStatus.Active)
			 .OrderBy(x => x.Description)
			 .AsQueryable();

			if (filter.CategoryId != null)
			{
				data = data.Where(x => x.ReasonCategoryId == filter.CategoryId);
			}

			data = SpecifyInclude(data);

			return data;
		}

		protected override Reason Get(long id)
		{
			var data = context.Set<Reason>()
				 .Include(x => x.ReasonCategory)
			.FirstOrDefault(x => x.Id == id);

			return data;
		}

		protected override IQueryable<Reason> SpecifyInclude(IQueryable<Reason> query)
		{
			//query = query

			//    .Include(x => x.SubDistrict);

			return query;
		}
	}
}
