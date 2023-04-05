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
	public class StaffManagerQueryRepository : QueryRepository<StaffManager, StaffManagerFilter>,
		 IQueryRepository<StaffManager, StaffManagerFilter>
	{
		public StaffManagerQueryRepository(VMMCStockManagementDbContext context) : base(context)
		{
		}
		protected override StaffManager GetByAny(long id)
		{
			throw new NotImplementedException();
		}
		protected override IQueryable<StaffManager> FilterData(StaffManagerFilter filter)
		{
			var data = context.Set<StaffManager>()
			 .Where(x => x.Status == EntityStatus.Active)
			 .AsQueryable();

			data = SpecifyInclude(data);

			return data;
		}

		protected override StaffManager Get(long id)
		{
			var data = context.Set<StaffManager>()
			.FirstOrDefault(x => x.Id == id);

			return data;
		}

		protected override IQueryable<StaffManager> SpecifyInclude(IQueryable<StaffManager> query)
		{
			query = query;

			return query;
		}
	}
}
