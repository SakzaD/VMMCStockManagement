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
	public class CategoryQueryRepository : QueryRepository<Category, CategoryFilter>,
			IQueryRepository<Category, CategoryFilter>
	{
		public CategoryQueryRepository(VMMCStockManagementDbContext context) : base(context)
		{
		}

		protected override IQueryable<Category> FilterData(CategoryFilter filter)
		{
			var data = context.Set<Category>()
			 .Where(x => x.Status == EntityStatus.Active)
			 .AsQueryable();	

			data = SpecifyInclude(data);

			return data;
		}

		protected override Category Get(long id)
		{
			var data = context.Set<Category>()
			.FirstOrDefault(x => x.Id == id);

			return data;
		}

		protected override Category GetByAny(long id)
		{
			throw new NotImplementedException();
		}

		protected override IQueryable<Category> SpecifyInclude(IQueryable<Category> query)
		{
			//query = query

			//    .Include(x => x.SubDistrict);

			return query;
		}
	}
}
