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
	public class DepartmentQueryRepository : QueryRepository<Department, DepartmentFilter>,
		IQueryRepository<Department, DepartmentFilter>
	{
		public DepartmentQueryRepository(VMMCStockManagementDbContext context) : base(context)
		{
		}
		protected override Department GetByAny(long id)
		{
			throw new NotImplementedException();
		}
		protected override IQueryable<Department> FilterData(DepartmentFilter filter)
		{
			var data = context.Set<Department>()
			 .Where(x => x.Status == EntityStatus.Active)
			 .AsQueryable();

			//if (filter.SubDistrictId != null)
			//{
			//    data = data.Where(x => x.SubDistrictId == filter.SubDistrictId);
			//}

			data = SpecifyInclude(data);

			return data;
		}

		protected override Department Get(long id)
		{
			var data = context.Set<Department>()
			.FirstOrDefault(x => x.Id == id);

			return data;
		}

		protected override IQueryable<Department> SpecifyInclude(IQueryable<Department> query)
		{
			//query = query

			//    .Include(x => x.SubDistrict);

			return query;
		}
	}
}
