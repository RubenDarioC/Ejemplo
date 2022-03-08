using EjemploBussiness.Interfaces;
using EjemploRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProyectoEjemplo.Controllers
{
	[RoutePrefix("api/Values")]
	public class ValuesController : ApiController
	{
		private readonly IUserServices UserServices;
		public ValuesController(IUserServices userServices) 
		{
			this.UserServices = userServices;
		}

		// GET api/values
		[HttpGet]
		[Route("Usuarios")] 
		public IHttpActionResult GetUsuarios([FromUri]string id)
		{
			var result = UserServices.GetUser();
			InternalServerError();
			NotFound();
			BadRequest();
			return Ok(result);
		}

		// GET api/values/5
		public string Get(int id)
		{
			return "value";
		}

		// POST api/values
		public void Post([FromBody] string value)
		{
		}

		// PUT api/values/5
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/values/5
		public void Delete(int id)
		{
		}
	}
}
