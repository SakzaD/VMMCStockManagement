using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Interfaces.Repositories;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Infrastructure.DbContexts;

namespace VMMCStockManagement.Infrastructure.Repositories.BaseRepositories
{
	public abstract class QueryRepository<Ent, Fil> : IQueryRepository<Ent, Fil> where Ent : BaseEntity where Fil : BaseFilter
	{
		protected readonly VMMCStockManagementDbContext context;

		public QueryRepository(VMMCStockManagementDbContext context)
		{
			this.context = context;
		}

		public Ent GetById(long id)
		{			
			var entity = Get(id);
			entity = SpecifyInclude(entity);
			return entity;
		}

		protected abstract Ent Get(long id);

		public IQueryable<Ent> GetAll()
		{
			var items = context.Set<Ent>()
					.AsNoTracking()
					.AsQueryable();

			return items;
		}

		public IQueryable<Ent> Filter(Fil filter)
		{
			var results = FilterData(filter);

			results = SpecifyInclude(results);

			return results;
		}

		protected abstract IQueryable<Ent> FilterData(Fil filter);

		protected virtual Ent SpecifyInclude(Ent entity)
		{
			return entity;
		}

		protected virtual IQueryable<Ent> SpecifyInclude(IQueryable<Ent> query)
		{
			return query;
		}

		public Ent GetBy(long id)
		{
			var entity = GetByAny(id);
			entity = SpecifyInclude(entity);
			return entity;
		}

		protected abstract Ent GetByAny(long id);
	}
}
