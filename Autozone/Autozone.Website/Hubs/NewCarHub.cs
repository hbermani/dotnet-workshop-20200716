using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Autozone.Website.Hubs {
	public class NewCarHub : Hub {
		public async Task SendMessage(string user, string message) {
			await Clients.All.SendAsync("ReceiveMessage", user, message);
		}
	}
}
