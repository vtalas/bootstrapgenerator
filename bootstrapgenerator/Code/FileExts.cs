using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using log4net;

namespace bootstrapgenerator.Code
{
	public class FileExts
	{
		static readonly ILog Log = LogManager.GetLogger(typeof(FileExts));
		
		public static string GetContent(string file)
		{
			if (File.Exists(file))
			{
				var content = File.ReadAllText(file);
				return content;
			}

			Log.ErrorFormat("{0} not exist", file);
			throw new FileNotFoundException(file);
		}

		public static void SetContent(string file, string content)
		{
			using (var target = new StreamWriter(file))
			{
				target.Write(content);
			}
		}

		public static void SetContent(string file, JObject data)
		{
			using (TextWriter writer = File.CreateText(file))
			{
				data.WriteTo(new JsonTextWriter(writer));
			}
		}

	}

}