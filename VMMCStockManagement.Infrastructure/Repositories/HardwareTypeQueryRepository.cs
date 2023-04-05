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
	public class HardwareTypeQueryRepository : QueryRepository<HardwareType, HardwareTypeFilter>,
		 IQueryRepository<HardwareType, HardwareTypeFilter>
	{
		public HardwareTypeQueryRepository(VMMCStockManagementDbContext context) : base(context)
		{
		}
		protected override HardwareType GetByAny(long id)
		{
			throw new NotImplementedException();
		}
		protected override IQueryable<HardwareType> FilterData(HardwareTypeFilter filter)
		{
			var data = context.Set<HardwareType>()
			 .Where(x => x.Status == EntityStatus.Active)
			 .AsQueryable();

			//if (filter.SubDistrictId != null)
			//{
			//    data = data.Where(x => x.SubDistrictId == filter.SubDistrictId);
			//}

			data = SpecifyInclude(data);

			return data;
		}

		protected override HardwareType Get(long id)
		{
			var data = context.Set<HardwareType>()
			.FirstOrDefault(x => x.Id == id);

			return data;
		}

		protected override IQueryable<HardwareType> SpecifyInclude(IQueryable<HardwareType> query)
		{
			//query = query

			//    .Include(x => x.SubDistrict);

			return query;
		}
	}
}
