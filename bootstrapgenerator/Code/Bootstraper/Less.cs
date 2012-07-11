using System.Text;
using Newtonsoft.Json.Linq;

namespace bootstrapgenerator.Code.Bootstraper
{

	public static class JObjectExt
	{
		public static string ToLess(this JObject source)
		{
			var x = new StringBuilder();
			foreach (var item in source)
			{
				x.Append("@"+item.Key)
					.Append(":")
					.Append(item.Value["value"]+";")
					.AppendLine();

			}
			return x.ToString();
		}
	}
}