using System;
using SignalR;
using SignalR.Hubs;

namespace bootstrapgenerator.Code.SignalR
{
	[HubName("bootstrapHub")]
	public class BootstrapHub :Hub
	{
		readonly BootstrapRefresher _refresher;
		string SessionId { get { return Context.RequestCookies["ASP.NET_SessionId"].Value; } }

        public BootstrapHub() : this(BootstrapRefresher.Instance) { }

		public BootstrapHub(BootstrapRefresher refresher)
        {
			_refresher = refresher;
        }

		public void Init()
		{
			_refresher.Init(SessionId, Context);
		}

		public void Refresh()
		{
			_refresher.Refresh(SessionId);
		}
	}

	public class BootstrapRefresher
	{
		readonly static Lazy<BootstrapRefresher> _instance = new Lazy<BootstrapRefresher>(() => new BootstrapRefresher());

		public static BootstrapRefresher Instance
		{
			get { return _instance.Value; }
		}

		public void Refresh(string id)
		{
			GetClients(id).refreshLess();
		}

		public void Init(string id, HubCallerContext context )
		{
			GetGroups().Add(context.ConnectionId, id);
		}

		static IGroupManager GetGroups()
		{
			return  GlobalHost.ConnectionManager.GetHubContext<BootstrapHub>().Groups;
		}

		static dynamic GetClients(string id)
		{
			return GlobalHost.ConnectionManager.GetHubContext<BootstrapHub>().Clients[id];
		}

		static dynamic GetClients()
		{
			return  GlobalHost.ConnectionManager.GetHubContext<BootstrapHub>().Clients;
		}


	}
}