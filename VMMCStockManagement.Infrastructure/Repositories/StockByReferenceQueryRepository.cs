using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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
	public class StockByReferenceQueryRepository : QueryRepository<StockByReference, StockByReferenceFilter>,
		IQueryRepository<StockByReference, StockByReferenceFilter>
	{
		public StockByReferenceQueryRepository(VMMCStockManagementDbContext context) : base(context)
		{
		}

		protected override IQueryable<StockByReference> FilterData(StockByReferenceFilter filter)
		{
			var data = context.Set<StockByReference>()
			 .Where(x => x.Status == EntityStatus.Active)
			 .AsQueryable();

			data = data.Where(x => x.ReferenceNumber == filter.ReferenceNumber);
			data = SpecifyInclude(data);

			return data;
		}

		protected override StockByReference Get(long id)
		{
			var data = context.Set<StockByReference>()
			.FirstOrDefault(x => x.Id == id);
			return data;
		}

		protected override StockByReference GetByAny(long id)
		{
			throw new NotImplementedException();
		}

		protected override IQueryable<StockByReference> SpecifyInclude(IQueryable<StockByReference> query)
		{
			query = query
				//.Include(x => x.UserAsset )
				.Include(x => x.Stock)
				.ThenInclude(x => x.Grant)
			   .Include(x => x.Stock)
			   .ThenInclude(x => x.Category)
			   .Include(x => x.Stock)
			   .ThenInclude(x => x.Model)
			   .ThenInclude(x => x.Make);


			return query;
		}
	}
}
