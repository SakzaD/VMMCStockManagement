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
	public class StockQueryRepository : QueryRepository<Stock, StockFilter>,
		 IQueryRepository<Stock, StockFilter>
	{
		public StockQueryRepository(VMMCStockManagementDbContext context) : base(context)
		{
		}
		protected override Stock GetByAny(long id)
		{
			throw new NotImplementedException();
		}
		protected override IQueryable<Stock> FilterData(StockFilter filter)
		{
			var data = context.Set<Stock>()
			 .Where(x => x.Status == EntityStatus.Active)
			 .Include(x => x.Model)
			.ThenInclude(x => x.Make)
			 .Include(x => x.Category)
			 .AsQueryable();

			if (filter.SearchValue != null)
			{
				data = data.Where(x => x.SerialNumber == filter.SearchValue || x.RegistrationNumber == filter.SearchValue);
			}

			data = SpecifyInclude(data);

			return data;
		}
		protected override Stock Get(long id)
		{
			var data = context.Set<Stock>()
			.Include(x => x.Category)
			.Include(x => x.Model)
			.ThenInclude(x => x.Make)
			.Include(x => x.Grant)
			.Include(x => x.Facility)
			.Include(x => x.Grant)
			.Include(x => x.Department)
			.Include(x => x.Supplier)
			.FirstOrDefault(x => x.Id == id);

			data = SpecifyInclude(data);
			return data;
		}
		protected override IQueryable<Stock> SpecifyInclude(IQueryable<Stock> query)
		{
			query = query

				.Include(x => x.Grant)
					.Include(x => x.Category)
				.Include(x => x.Model)
				.ThenInclude(x => x.Make);

			return query;
		}
	}
}
