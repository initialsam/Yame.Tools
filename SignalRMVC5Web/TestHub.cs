using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SignalRMVC5Web
{
    [HubName("TestHub")]
    public class TestHub : Hub
    {
        public void Join(string groupName)
        {
            Groups.Add(Context.ConnectionId, groupName);
        }

        public void Send(string groupName, string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            //Clients.All.addNewMessageToPage(name, message);
            Clients.Group(groupName).addNewMessageToPage(name, message);
        }
    }
}