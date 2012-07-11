using System.IO;
using Newtonsoft.Json.Linq;
using bootstrapgenerator.Code.Bootstraper.DataRepository;
using bootstrapgenerator.Code.Bootstraper.Less;
using cms.Code.Bootstraper;
using cms.Code.Bootstraper.DataRepository;

namespace bootstrapgenerator.Code.Bootstraper
{
	public class BootstrapGenerator
	{
		BootstrapDataRepository Repository { get; set; }
		string UserId { get; set; }
		string BasePath { get; set; }

		string UserVariablesLessFile {get
		{
			return Path.Combine(BasePath, string.Format(@"userdata\variables_{0}.less", UserId));
		}}

		public BootstrapGenerator(string basePath, string userid, BootstrapDataRepository repository)
		{
			UserId = userid;
			BasePath = basePath;
			Repository = repository;
		}

		public BootstrapGenerator(string basePath, string userid):this(basePath,userid,new BootstrapDataRepositoryImpl(basePath))
		{
		}

		public string GetUserBootstrapCss(ILessCompiler lesak, bool minify)
		{
			if (!File.Exists(UserVariablesLessFile))
			{
				var defaultvars = Path.Combine(BasePath, @"less\variables.less");
				var content = FileExts.GetContent(defaultvars);
				FileExts.SetContent(UserVariablesLessFile, content);
			}
			var lessfile = Path.Combine(BasePath, string.Format(@"userdata\bootstrap_{0}.less", UserId));

			if (!File.Exists(lessfile))
			{
				var defaultboot = Path.Combine(BasePath, @"less\bootstrap.less");
				var content = FileExts.GetContent(defaultboot);
				var updatedBoot = content.Replace("less/variables.less", string.Format("userdata/variables_{0}.less", UserId));
				FileExts.SetContent(lessfile, updatedBoot);
			}

			return lesak.GenerateCss(lessfile, minify);
		}



		public JObject GetUserVariablesJson()
		{
			return Repository.Get(UserId);
		}

		public void SetUserVariablesJson(JObject jsonData)
		{
			Repository.Save(UserId, jsonData);
			var lesccontent = jsonData.ToLess();
			FileExts.SetContent(UserVariablesLessFile, lesccontent);
		}
	}
}