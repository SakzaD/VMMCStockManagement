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
	public class StockRequestQueryReportRepository : QueryRepository<StockRequest, StockRequestReportFilter>,
		IQueryRepository<StockRequest, StockRequestReportFilter>
	{
		public StockRequestQueryReportRepository(VMMCStockManagementDbContext context) : base(context)
		{
		}

		protected override StockRequest GetByAny(long id)
		{
			throw new NotImplementedException();
		}

		protected override IQueryable<StockRequest> FilterData(StockRequestReportFilter filter)
		{
			var data = context.Set<StockRequest>()
			 .Where(x => x.Status == EntityStatus.Active)
			 .Include(x => x.Manager)
			 .Include(x => x.RequestApproval)			
			 .OrderByDescending(x => x.CreatedAt)
			 .AsQueryable();


			if (filter.DepartmentId != null)
			{
				data = data.Where(x => x.DepartmentId == filter.DepartmentId);
			}

			if (filter.JobTitleId != null)
			{
				data = data.Where(x => x.JobTitleId == filter.JobTitleId);
			}

			if (filter.GrantId != null)
			{
				data = data.Where(x => x.JobTitleId == filter.JobTitleId);
			}

			if (filter.StartDate != null)
			{
				data = data.Where(x => x.CreatedAt == filter.StartDate);
			}

			if (filter.EndDate != null)
			{
				data = data.Where(x => x.CreatedAt == filter.EndDate);
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
				.Include(x => x.StockRequestAssetCategories)
				.ThenInclude(x => x.Category)
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
			//.Include(x => x.AssetRequestHardware);

			return query;
		}
	}
}
