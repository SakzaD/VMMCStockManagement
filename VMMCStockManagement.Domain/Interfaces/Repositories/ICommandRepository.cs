using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;

namespace VMMCStockManagement.Domain.Interfaces.Repositories
{
	public interface ICommandRepository<Ent> where Ent : BaseEntity
	{
		Ent Create(Ent entity);
		Ent GetById(long id);
		bool Delete(long id, bool isPermanentDelete = false);
		bool Delete(Ent entity, bool isPermanentDelete = false);
		Ent Update(Ent entity);


	}
}
