using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CookieAuthMVC5
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "MyCookieNameTODO",
                LoginPath = new PathString("/Login"),
                ExpireTimeSpan = TimeSpan.FromDays(1),

                ReturnUrlParameter = "ReturnUrl",
                CookiePath = "/",
                //ExpireTimeSpan = TimeSpan.FromDays(14.0),
                SlidingExpiration = true,
                CookieHttpOnly = true,
                CookieSecure = CookieSecureOption.SameAsRequest
            });
        }
    }
}