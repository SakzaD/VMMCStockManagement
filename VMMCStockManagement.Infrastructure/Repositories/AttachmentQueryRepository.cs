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
	public class AttachmentQueryRepository : QueryRepository<Attachment, AttachmentFilter>,
	   IQueryRepository<Attachment, AttachmentFilter>
	{
		public AttachmentQueryRepository(VMMCStockManagementDbContext context) : base(context)
		{
		}
		protected override Attachment GetByAny(long id)
		{
			throw new NotImplementedException();
		}
		protected override IQueryable<Attachment> FilterData(AttachmentFilter filter)
		{
			var data = context.Set<Attachment>()
			 .Where(x => x.Status == EntityStatus.Active)
			 .AsQueryable();

			data = SpecifyInclude(data);

			return data;
		}

		protected override Attachment Get(long id)
		{
			var data = context.Set<Attachment>()

			.FirstOrDefault(x => x.Id == id);

			return data;
		}

		protected override IQueryable<Attachment> SpecifyInclude(IQueryable<Attachment> query)
		{
			query = query;

			return query;
		}
	}
}
