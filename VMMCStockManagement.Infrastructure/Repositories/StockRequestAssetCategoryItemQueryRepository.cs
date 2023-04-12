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
	public class StockRequestAssetCategoryItemQueryRepository : QueryRepository<StockRequestAssetCategoryItem, StockRequestAssetCategoryItemFilter>,
	   IQueryRepository<StockRequestAssetCategoryItem, StockRequestAssetCategoryItemFilter>
	{
		public StockRequestAssetCategoryItemQueryRepository(VMMCStockManagementDbContext context) : base(context)
		{
		}
		protected override StockRequestAssetCategoryItem GetByAny(long id)
		{
			throw new NotImplementedException();
		}
		protected override IQueryable<StockRequestAssetCategoryItem> FilterData(StockRequestAssetCategoryItemFilter filter)
		{
			var data = context.Set<StockRequestAssetCategoryItem>()
			 .Where(x => x.Status == EntityStatus.Active)

			 //.Include(x => x.AssetRequestAssetCategories)
			 .OrderByDescending(x => x.CreatedAt)
			 .AsQueryable();

			data = SpecifyInclude(data);

			return data;
		}

		protected override StockRequestAssetCategoryItem Get(long id)
		{
			var data = context.Set<StockRequestAssetCategoryItem>()

			.FirstOrDefault(x => x.Id == id);

			return data;
		}

		protected override IQueryable<StockRequestAssetCategoryItem> SpecifyInclude(IQueryable<StockRequestAssetCategoryItem> query)
		{
			query = query;

			//.Include(x => x.AssetRequestHardware);

			return query;
		}
	}
}
