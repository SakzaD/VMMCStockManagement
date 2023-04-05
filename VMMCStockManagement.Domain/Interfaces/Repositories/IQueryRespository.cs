using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Models.Requests.Filters;

namespace VMMCStockManagement.Domain.Interfaces.Repositories
{
	public interface IQueryRepository<Ent, Fil> where Ent : BaseEntity where Fil : BaseFilter
	{

		IQueryable<Ent> Filter(Fil filter);
		Ent GetById(long id);
		Ent GetBy(long id);
		IQueryable<Ent> GetAll();
	}
}
