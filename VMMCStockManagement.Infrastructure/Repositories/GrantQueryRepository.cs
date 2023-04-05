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
	public class GrantQueryRepository : QueryRepository<Grant, GrantFilter>,
		 IQueryRepository<Grant, GrantFilter>
	{
		public GrantQueryRepository(VMMCStockManagementDbContext context) : base(context)
		{
		}
		protected override Grant GetByAny(long id)
		{
			throw new NotImplementedException();
		}
		protected override IQueryable<Grant> FilterData(GrantFilter filter)
		{
			var data = context.Set<Grant>()
			 .Where(x => x.Status == EntityStatus.Active)
			 .AsQueryable();

			//if (filter.SubDistrictId != null)
			//{
			//    data = data.Where(x => x.SubDistrictId == filter.SubDistrictId);
			//}

			data = SpecifyInclude(data);

			return data;
		}

		protected override Grant Get(long id)
		{
			var data = context.Set<Grant>()
			.FirstOrDefault(x => x.Id == id);

			return data;
		}

		protected override IQueryable<Grant> SpecifyInclude(IQueryable<Grant> query)
		{
			query = query

				.Include(x => x.Country);

			return query;
		}
	}
}
