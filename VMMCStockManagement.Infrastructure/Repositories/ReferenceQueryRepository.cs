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
	public class ReferenceQueryRepository : QueryRepository<Reference, ReferenceFilter>,
		 IQueryRepository<Reference, ReferenceFilter>
	{
		public ReferenceQueryRepository(VMMCStockManagementDbContext context) : base(context)
		{
		}
		protected override Reference GetByAny(long id)
		{
			throw new NotImplementedException();
		}
		protected override IQueryable<Reference> FilterData(ReferenceFilter filter)
		{
			var data = context.Set<Reference>()
			 .Where(x => x.Status == EntityStatus.Active)
			 .AsQueryable();			

			data = SpecifyInclude(data);

			return data;
		}

		protected override Reference Get(long id)
		{
			var data = context.Set<Reference>()
			.FirstOrDefault(x => x.Id == id);

			return data;
		}

		protected override IQueryable<Reference> SpecifyInclude(IQueryable<Reference> query)
		{
			//query = query

			//    .Include(x => x.SubDistrict);

			return query;
		}
	}
}
