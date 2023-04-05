using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Enums;
using VMMCStockManagement.Domain.Interfaces.Repositories;
using VMMCStockManagement.Infrastructure.DbContexts;

namespace VMMCStockManagement.Infrastructure.Repositories.BaseRepositories
{
	public class CommandRepository<T> : ICommandRepository<T> where T : BaseEntity
	{
		protected readonly VMMCStockManagementDbContext context;

		public CommandRepository(VMMCStockManagementDbContext context)
		{
			this.context = context;
		}


		public T Create(T entity)
		{
			entity.Status = EntityStatus.Active;
			entity.CreatedAt = DateTime.Now;
			entity.CreatedAt = DateTime.Now;
			context.Set<T>().Add(entity);
			context.SaveChanges();
			return entity;
		}

		public bool Delete(long id, bool isPermanetDelete = false)
		{
			var entity = context.Set<T>().AsNoTracking()
					.FirstOrDefault(e => e.Id == id);

			if (entity == null)
				return false;

			entity.UpdatedAt = DateTime.Now;
			entity.Status = EntityStatus.Deleted;

			if (isPermanetDelete)
			{
				context.Set<T>()
				.Remove(entity);
			}

			context.SaveChanges();

			return entity.Status == EntityStatus.Deleted;
		}

		public bool Delete(T entity, bool isPermanetDelete = false)
		{
			entity.Status = EntityStatus.Deleted;
			entity.UpdatedAt = DateTime.Now;

			if (isPermanetDelete)
			{
				var res = context.Set<T>()
				.Remove(entity);
			}

			context.SaveChanges();
			return true;
		}

		public T GetById(long id)
		{
			var entity = context.Set<T>()
				.AsNoTracking()
				.Where(e => e.Status == EntityStatus.Active)
				.FirstOrDefault(e => e.Id == id);

			return entity;
		}

		public IQueryable<T> GetAll()
		{
			var items = context.Set<T>()
					.AsNoTracking()
					.AsQueryable();

			return items;
		}

		public T Update(T entity)
		{
			entity.UpdatedAt = DateTime.Now;
			var item = context.Update(entity);

			context.Entry(entity).Property(x => x.CreatedAt).IsModified = false;
			context.SaveChanges();

			return item.State != EntityState.Unchanged ?
				null : item.Entity;
		}
	}
}
