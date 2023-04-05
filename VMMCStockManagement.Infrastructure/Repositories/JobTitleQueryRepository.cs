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
	public class JobTitleQueryRepository : QueryRepository<JobTitle, JobTitleFilter>,
		IQueryRepository<JobTitle, JobTitleFilter>
	{
		public JobTitleQueryRepository(VMMCStockManagementDbContext context) : base(context)
		{
		}
		protected override JobTitle GetByAny(long id)
		{
			throw new NotImplementedException();
		}
		protected override IQueryable<JobTitle> FilterData(JobTitleFilter filter)
		{
			var data = context.Set<JobTitle>()
			 .Where(x => x.Status == EntityStatus.Active)
			 .AsQueryable();

			//if (filter.SubDistrictId != null)
			//{
			//    data = data.Where(x => x.SubDistrictId == filter.SubDistrictId);
			//}

			data = SpecifyInclude(data);

			return data;
		}

		protected override JobTitle Get(long id)
		{
			var data = context.Set<JobTitle>()
			.FirstOrDefault(x => x.Id == id);

			return data;
		}

		protected override IQueryable<JobTitle> SpecifyInclude(IQueryable<JobTitle> query)
		{
			//query = query

			//    .Include(x => x.SubDistrict);

			return query;
		}
	}
}
