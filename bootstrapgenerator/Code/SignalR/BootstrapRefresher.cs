using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SignalR.Hubs;
using bootstrapgenerator.Code.Bootstraper;
using log4net;

namespace bootstrapgenerator.Code.SignalR
{
	[HubName("bootstrapRefresher")]
	public class BootstrapRefresher :Hub
	{
		public BootstrapRefresher()
		{
			//Bg = new BootstrapGeneratorFactory(requestContext).Instance;
		}

		public string test()
		{
			return _stockTicker.MarketState.ToString();
		}

	}

}