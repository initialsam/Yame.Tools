﻿using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(WebApi2.Startup))]

namespace WebApi2
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //GlobalHost.DependencyResolver.UseRedis("127.0.0.1", 6379, null, "PrivateRoomHub");
            //GlobalHost.DependencyResolver.UseStackExchangeRedis("127.0.0.1", 6379, null, "AppName");
            app.MapSignalR();
            //app.MapAzureSignalR("eva01");
        }
    }
}