using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading.Tasks;
using System.Threading;

namespace SignalRSample.SignalR
{
	[HubName("progress")]
	public class ProgressHub : Hub
	{
		public int Count = 1;

		public static void SendMessage(string message, int count)
		{
			var hubContext = GlobalHost.ConnectionManager.GetHubContext<ProgressHub>();
			hubContext.Clients.All.sendMessage(string.Format(message), count);

		}

		public void GetCountAndMessage(string message)
		{
			StartProgress();
			Clients.Caller.sendMessage(string.Format(message), Count);
		}

		private void StartProgress()
		{
			Task.Run(() => {
				for (int i = 0; i <= 100; i++) {
					Thread.Sleep(100);
					SendMessage("処理中...", i);
				}
				SendMessage("おわったで！", 100);
			});
		}
		
	}
}