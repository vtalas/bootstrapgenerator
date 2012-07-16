using System.Web.Mvc;
using Newtonsoft.Json;

namespace bootstrapgenerator.Code
{
	public class JSONNetResult : ActionResult
	{
		private readonly object _data;
		public JSONNetResult(object data)
		{
			_data = data;
		}

		public override void ExecuteResult(ControllerContext context)
		{
			var response = context.HttpContext.Response;
			response.ContentType = "application/json";
			var settings = new JsonSerializerSettings()
			               	{
			               		ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			               	};

			response.Write(JsonConvert.SerializeObject(_data, Formatting.Indented, settings));
		}
	}
}