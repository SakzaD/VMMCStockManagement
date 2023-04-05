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
	public class HardwareModelQueryRepository : QueryRepository<HardwareModel, HardwareModelFilter>,
		IQueryRepository<HardwareModel, HardwareModelFilter>
	{
		public HardwareModelQueryRepository(VMMCStockManagementDbContext context) : base(context)
		{
		}
		protected override HardwareModel GetByAny(long id)
		{
			throw new NotImplementedException();
		}
		protected override IQueryable<HardwareModel> FilterData(HardwareModelFilter filter)
		{
			var data = context.Set<HardwareModel>()
			 .Where(x => x.Status == EntityStatus.Active)
			 .AsQueryable();

			//if (filter.SubDistrictId != null)
			//{
			//    data = data.Where(x => x.SubDistrictId == filter.SubDistrictId);
			//}

			data = SpecifyInclude(data);

			return data;
		}

		protected override HardwareModel Get(long id)
		{
			var data = context.Set<HardwareModel>()
			.FirstOrDefault(x => x.Id == id);

			return data;
		}

		protected override IQueryable<HardwareModel> SpecifyInclude(IQueryable<HardwareModel> query)
		{
			//query = query

			//    .Include(x => x.SubDistrict);

			return query;
		}
	}
}
