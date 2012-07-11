using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace angular
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "Bootstrap", action = "Index", id = UrlParameter.Optional } // Parameter defaults
			);

		}

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);
		}

		protected void Session_Start(Object sender, EventArgs e)
		{
			/*
			 When using cookie-based session state, ASP.NET does not allocate storage for session data until the Session object is used. As a result, a new session ID is generated for each page request until the session object is accessed. If your application requires a static session ID for the entire session, you can either implement the Session_Start method in the application's Global.asax file and store data in the Session object to fix the session ID, or you can use code in another part of your application to explicitly store data in the Session object.
			 */
			Session["init"] = 0;
		}

	}
}