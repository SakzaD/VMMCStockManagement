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
	public class UserAssetQueryRepository : QueryRepository<UserAsset, UserAssetFilter>,
	  IQueryRepository<UserAsset, UserAssetFilter>
	{
		public UserAssetQueryRepository(VMMCStockManagementDbContext context) : base(context)
		{
		}
		protected override UserAsset GetByAny(long id)
		{
			var data = context.Set<UserAsset>()

			 .Include(x => x.User)
			 .Include(x => x.Stock)
			 .ThenInclude(x => x.Category)
			 .Include(x => x.Stock)
			 .ThenInclude(x => x.Model)
			 .ThenInclude(x => x.Make)
			 .FirstOrDefault(x => x.Status == EntityStatus.Active && x.IsReturned == false && x.AssetId == id);

			data = SpecifyInclude(data);

			return data;
		}
		protected override IQueryable<UserAsset> FilterData(UserAssetFilter filter)
		{
			var data = context.Set<UserAsset>()
			 .Where(x => x.Status == EntityStatus.Active && x.IsReturned == false)
			 .Include(x => x.User)
			 .Include(x => x.Stock)
			 .ThenInclude(x => x.Category)
			 .AsQueryable();

			if (filter.SearchValue != null)
			{
				data = data.Where(x => x.Stock.SerialNumber == filter.SearchValue
				|| x.Stock.RegistrationNumber == filter.SearchValue);
			}

			data = SpecifyInclude(data);

			return data;
		}
		protected override UserAsset Get(long id)
		{
			var data = context.Set<UserAsset>()
			.Include(x => x.User)
			.Include(x => x.Stock)
			.ThenInclude(x => x.Category)
			.FirstOrDefault(x => x.Id == id);

			return data;
		}

		protected UserAsset GetByAssetId(long id)
		{
			var data = context.Set<UserAsset>()
			.Include(x => x.User)
			.Include(x => x.Stock)
			.ThenInclude(x => x.Category)
			.FirstOrDefault(x => x.AssetId == id);

			return data;
		}

		protected override IQueryable<UserAsset> SpecifyInclude(IQueryable<UserAsset> query)
		{
			query = query
		   .Include(x => x.User)
			.Include(x => x.Stock)
			.ThenInclude(x => x.Category);


			return query;
		}
	}
}
