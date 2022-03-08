using Domain;
using EjemploBussiness.Interfaces;
using EjemploRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemploBussiness.Services
{
	public class UserServices : IUserServices
	{
		private readonly IUserDal UserDal;
		public UserServices(IUserDal userDal)
		{
			this.UserDal = userDal;
		}

		public string GetUser()
		{ 
			string letrasOpcionales = string.Empty;
			Users users = this.UserDal.GetUserByFilter();
			return  users.Name;

		}

	}
}
