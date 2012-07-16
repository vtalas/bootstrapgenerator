using System;
using System.IO;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using bootstrapgenerator.Code;
using bootstrapgenerator.Code.Bootstraper;
using bootstrapgenerator.Code.Bootstraper.DataRepository;
using bootstrapgenerator.Code.Bootstraper.Less;
using bootstrapgenerator.Code.SignalR;
using cms.Code.Bootstraper.DataRepository;
using log4net;

namespace bootstrapgenerator.Controllers
{
	public class BootstrapController : Controller
    {
		static readonly ILog Log = LogManager.GetLogger(typeof(BootstrapController));
		
		BootstrapGenerator Bg { get; set; }

		protected override void Initialize(System.Web.Routing.RequestContext requestContext)
		{
			Bg = new BootstrapGeneratorFactory(requestContext).Instance;
			base.Initialize(requestContext);
		}

		public ActionResult Index()
        {
			return View(Bg.GetUserVariablesJson());
        }
		public ActionResult Jsondata()
        {
			return new JSONNetResult(Bg.GetUserVariablesJson());
		}

		public ActionResult PreviewMode()
        {
			return View(Bg.GetUserVariablesJson());
        }

		public ActionResult Preview()
        {
			return View();
        }

		public ActionResult UserBootstrap(bool? compress)
		{
			var c = compress.HasValue && compress.Value;

			var lesak = new Lessaak(new ResponseLogger(Response), new ServerPathResolver(Server));
			
			var css = Bg.GetUserBootstrapCss(lesak, c);
			
			Response.ContentType = "text/css";
			Response.Write(css);
			Response.Write(Session.SessionID);

        	return new EmptyResult();
        }

		
		[JObjectFilter(Param = "data")]
		public ActionResult Refresh(JObject data)
		{
			Bg.SetUserVariablesJson(data);
			BootstrapRefresher.Instance.Refresh(HttpContext.Session.SessionID);
			return new EmptyResult();
		}


		 public class JObjectFilter : ActionFilterAttribute {

			public string Param { get; set; }

			public override void OnActionExecuting(ActionExecutingContext filterContext)
			{
				if ((filterContext.HttpContext.Request.ContentType ?? string.Empty).Contains("application/json"))
				{
					var stream = filterContext.HttpContext.Request.InputStream;
					stream.Position = 0;

					var streamReader = new StreamReader(stream);
					
					var x = JsonConvert.DeserializeObject<JObject>(streamReader.ReadToEnd());
					filterContext.ActionParameters[Param] = x;
				}
			}
		 }



    }
}
