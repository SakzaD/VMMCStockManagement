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
	public class StockRequestQueryRepository : QueryRepository<StockRequest, StockRequestFilter>,
		IQueryRepository<StockRequest, StockRequestFilter>
	{
		public StockRequestQueryRepository(VMMCStockManagementDbContext context) : base(context)
		{
		}
		protected override StockRequest GetByAny(long id)
		{
			throw new NotImplementedException();
		}
		protected override IQueryable<StockRequest> FilterData(StockRequestFilter filter)
		{
			var data = context.Set<StockRequest>()
			 .Where(x => x.Status == EntityStatus.Active)
			 .Include(x => x.Manager)
			 .Include(x => x.RequestApproval)
			 .OrderByDescending(x => x.CreatedAt)
			 .AsQueryable();

			if (filter.AccessRole == AccessRole.DataCapture)
			{
				data = data.Where(x => x.ManagerId == filter.UserId);
			}
			else if (filter.AccessRole == AccessRole.SiteManager)
			{
				data = data.Where(x => x.CreatedBy == filter.UserId);
			}			
			else if (filter.AccessRole == AccessRole.DistrictManager)
			{
				data = data.Where(x => x.RequestApproval != null &&
				x.RequestApproval.LineManagerStatus == StockStatus.Approved);
			}			

			data = SpecifyInclude(data);

			return data;
		}

		protected override StockRequest Get(long id)
		{
			var data = context.Set<StockRequest>()
				.Include(x => x.Manager)
				.Include(x => x.Requester)
				.Include(x => x.RequestApproval)
				.Include(x => x.JobTitle)
				.Include(x => x.Grant)
				.Include(x => x.Department)
				.Include(x => x.Facility)
				.Include(x => x.Country)
				.Include(x => x.Reason)
				.Include(x => x.StockRequestAssetCategories)
				.ThenInclude(x => x.Category)
				.Include(x => x.StockRequestAssetCategories)
				.ThenInclude(x => x.StockRequestAssetCategoryItems)
				.ThenInclude(x => x.Stock)
				.FirstOrDefault(x => x.Id == id);

			return data;
		}

		protected override IQueryable<StockRequest> SpecifyInclude(IQueryable<StockRequest> query)
		{
			query = query

				.Include(x => x.JobTitle)
				.Include(x => x.Grant)
				.Include(x => x.Department)
				.Include(x => x.Facility)
				.Include(x => x.Country)
				.Include(x => x.Country)
				.Include(x => x.StockRequestAssetCategories)
				.ThenInclude(x => x.Category)
				;
		
			return query;
		}
	}
}
