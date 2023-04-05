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
	public class ModelQueryRepository : QueryRepository<Model, ModelFilter>,
		IQueryRepository<Model, ModelFilter>
	{
		public ModelQueryRepository(VMMCStockManagementDbContext context) : base(context)
		{
		}
		protected override Model GetByAny(long id)
		{
			throw new NotImplementedException();
		}
		protected override IQueryable<Model> FilterData(ModelFilter filter)
		{
			var data = context.Set<Model>()
			 .Where(x => x.Status == EntityStatus.Active)
			 .AsQueryable();

			if (filter.Name != null)
			{
				data = data.Where(x => x.Name == filter.Name);
			}

			if (filter.MakeId != null)
			{
				data = data.Where(x => x.MakeId == filter.MakeId);
			}

			data = SpecifyInclude(data);

			return data;
		}

		protected override Model Get(long id)
		{
			var data = context.Set<Model>()
			.Include(x => x.Make)
			.FirstOrDefault(x => x.Id == id);

			return data;
		}

		protected override IQueryable<Model> SpecifyInclude(IQueryable<Model> query)
		{
			query = query

				.Include(x => x.Make);

			return query;
		}
	}
}
