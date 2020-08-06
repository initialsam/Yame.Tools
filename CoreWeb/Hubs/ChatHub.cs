using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Connections.Features;
using Microsoft.AspNetCore.SignalR;

namespace CoreWeb.Hubs
{
    public class ChatHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            //var transport = Context.QueryString.First(p => p.Key == "transport").Value;
            var transport = Context.Features.Get<IHttpTransportFeature>().TransportType;
            var a = ($"OnConnectedAsync ConnectionId:{Context.ConnectionId} , Transport : {transport}");

            Clients.All.SendAsync("DebugMonitor", a);



            return base.OnConnectedAsync();
        }

        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
