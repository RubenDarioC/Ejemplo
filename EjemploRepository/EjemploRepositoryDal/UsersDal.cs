using Domain;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EjemploRepository.Interfaces;

namespace EjemploRepository.EjemploRepositoryDal
{
	public class UsersDal : BaseRepositorySql<Users>, IUserDal
	{
		public Users GetUserId(long id) 
		{
			return this.GetById(id);
		}

		public Users GetUserByFilter() 
		{
			return this.table.SingleOrDefault(u => u.Name == "pepito");
		}
	}
}
