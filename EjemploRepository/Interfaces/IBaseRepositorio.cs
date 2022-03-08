using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemploRepository.Interfaces
{
	public interface IBaseRepositorio<T> where T : class
	{
		IEnumerable<T> GetAll();
		T GetById(params object[] id);
		//IEnumerable<T> GetCustomFilter(QueryParameters<T> _param, string[] includes = null);
		T Insert(T obj);
		void Update(T obj);
		void Delete(params object[] ids);
		void Save();
		IEnumerable<T> InsertRange(IEnumerable<T> objs);
		void Update(T obj, params object[] ids);
	}
}
