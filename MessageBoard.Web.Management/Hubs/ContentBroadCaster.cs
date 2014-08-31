using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageBoard.Web.Management.Hubs
{
	public class ContentBroadCaster
	{
		private readonly static Lazy<ContentBroadCaster> _instance = new Lazy<ContentBroadCaster>(() => new ContentBroadCaster(GlobalHost.ConnectionManager.GetHubContext<ContentHub>().Clients));
		public static ContentBroadCaster Instance
		{
			get
			{
				return _instance.Value;
			}
		}

		private ContentBroadCaster(IHubConnectionContext<dynamic> clients)
		{
			Clients = clients;

			// Remainder of constructor ...
		}

		private IHubConnectionContext<dynamic> Clients
		{
			get;
			set;
		}

		public void UpdateContent(int boardId)
		{
			Clients.All.refreshContent(boardId);
		}
	}
}