using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemploRepository.Interfaces
{
	public interface IUserDal : IBaseRepositorio<Users>
	{
		Users GetUserId(long id);
		Users GetUserByFilter();
	}
}
