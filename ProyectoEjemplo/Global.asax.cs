using EjemploBussiness.Interfaces;
using EjemploBussiness.Services;
using EjemploRepository.EjemploRepositoryDal;
using EjemploRepository.Interfaces;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ProyectoEjemplo
{
	public class WebApiApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			var container = new Container();
			container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
			//Bll
			container.Register<IUserServices, UserServices>();
			//Dal
			container.Register<IUserDal, UsersDal>();

			container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
			container.Verify();

			GlobalConfiguration.Configuration.DependencyResolver =
				new SimpleInjectorWebApiDependencyResolver(container);

		}
	}
}
