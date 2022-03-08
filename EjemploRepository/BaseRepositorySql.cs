using Domain;
using EjemploRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace EjemploRepository
{
	public class BaseRepositorySql<T> : IBaseRepositorio<T> where T : class
	{
		protected DbContext _context = null;
		protected DbSet<T> table = null;
		protected readonly ObjectSet<T> OBJECTSET;

		public BaseRepositorySql()
		{
			this._context = new Model1Container();
			table = _context.Set<T>();
			var objectContext = ((IObjectContextAdapter)_context).ObjectContext;
			OBJECTSET = objectContext.CreateObjectSet<T>();
		}
		public IEnumerable<T> GetAll()
		{
			return table.AsNoTracking().ToList<T>();
		}

		public T GetById(params object[] id)
		{
			return table.Find(id);
		}

		public virtual T Insert(T obj)
		{
			var respuesta = table.Add(obj);
			return respuesta;
		}

		public virtual IEnumerable<T> InsertRange(IEnumerable<T> objs)
		{
			table.AddRange(objs);
			return objs;
		}

		public virtual void Update(T obj)
		{
			_context.Entry(obj).State = EntityState.Detached;
			table.Attach(obj);
			_context.Entry(obj).State = EntityState.Modified;
		}

		public virtual void Update(T obj, params object[] ids)
		{
			T entity = table.Find(ids);
			var entry = _context.Entry(entity);
			foreach (var k in OBJECTSET.EntitySet.ElementType.KeyMembers.Select(k => k.Name))
			{
				var propperty = typeof(T).GetProperty(k);
				var currentKeyValue = propperty.GetValue(entity);
				propperty.SetValue(obj, currentKeyValue);
			};
			entry.CurrentValues.SetValues(obj);
		}
		public virtual void Delete(params object[] ids)
		{
			T existing = table.Find(ids);
			table.Remove(existing);
		}

		public void Save()
		{
			_context.SaveChanges();
		}

		//added by filter custom from bll
		//public IEnumerable<T> GetCustomFilter(QueryParameters<T> _param, string[] includes = null)
		//{
		//	try
		//	{
		//		Expression<Func<T, bool>> whereTrue = x => true;
		//		var where = (_param.Where == null) ? whereTrue : _param.Where;

		//		if (includes != null && includes.Any())
		//			return _context
		//				.Set<T>()
		//				.Where(where)
		//				.Include(string.Join(",", includes))
		//				.ToList();
		//		else
		//			return _context
		//				.Set<T>()
		//				.Where(where)
		//				.ToList();
		//	}
		//	catch (Exception)
		//	{
		//		throw;
		//	}
		//}
	}
}
