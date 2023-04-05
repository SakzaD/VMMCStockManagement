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
	public class RequestApprovalQueryRepository : QueryRepository<RequestApproval, RequestApprovalFilter>,
	   IQueryRepository<RequestApproval, RequestApprovalFilter>
	{
		public RequestApprovalQueryRepository(VMMCStockManagementDbContext context) : base(context)
		{
		}
		protected override RequestApproval GetByAny(long id)
		{
			throw new NotImplementedException();
		}
		protected override IQueryable<RequestApproval> FilterData(RequestApprovalFilter filter)
		{
			var data = context.Set<RequestApproval>()
			 .Where(x => x.Status == EntityStatus.Active)
			 .AsQueryable();			

			data = SpecifyInclude(data);

			return data;
		}

		protected override RequestApproval Get(long id)
		{
			var data = context.Set<RequestApproval>()
			.FirstOrDefault(x => x.Id == id);

			return data;
		}

		protected override IQueryable<RequestApproval> SpecifyInclude(IQueryable<RequestApproval> query)
		{		

			return query;
		}
	}
}
