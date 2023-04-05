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
	public class TicketQueryRepository : QueryRepository<Ticket, TicketFilter>,
		 IQueryRepository<Ticket, TicketFilter>
	{
		public TicketQueryRepository(VMMCStockManagementDbContext context) : base(context)
		{
		}
		protected override Ticket GetByAny(long id)
		{
			throw new NotImplementedException();
		}
		protected override IQueryable<Ticket> FilterData(TicketFilter filter)
		{
			var data = context.Set<Ticket>()
			 .Where(x => x.Status == EntityStatus.Active)
			 .AsQueryable();
			
			data = SpecifyInclude(data);

			return data;
		}

		protected override Ticket Get(long id)
		{
			var data = context.Set<Ticket>()
			.FirstOrDefault(x => x.Id == id);

			return data;
		}

		protected override IQueryable<Ticket> SpecifyInclude(IQueryable<Ticket> query)
		{	

			return query;
		}
	}
}
