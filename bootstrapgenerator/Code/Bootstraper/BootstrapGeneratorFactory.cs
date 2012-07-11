using System;
using System.Web.Routing;
using bootstrapgenerator.Code.Bootstraper.DataRepository;

namespace bootstrapgenerator.Code.Bootstraper
{
	public class BootstrapGeneratorFactory
	{
		string UserId { get; set; }
		string BasePath { get; set; }

		public BootstrapGeneratorFactory(RequestContext requestContext)
		{
			BasePath = requestContext.HttpContext.Server.MapPath("~/App_Data/bootstrapper");
			UserId = requestContext.HttpContext.Session != null ? requestContext.HttpContext.Session.SessionID : Guid.NewGuid().ToString();
		}

		public BootstrapGenerator Instance {get
		{
			return new BootstrapGenerator(BasePath, UserId, new BootstrapDataRepositoryImpl(BasePath));
		}}
	}
}