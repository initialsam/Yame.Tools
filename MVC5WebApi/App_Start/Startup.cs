using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using NSwag;
using NSwag.AspNet.Owin;
using NSwag.Generation.Processors.Security;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;

[assembly: OwinStartup(typeof(MVC5WebApi.App_Start.Startup))]
namespace MVC5WebApi.App_Start
{
    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            _ = app.Use(async (ctx, next) =>
            {
                if (ctx.Request.Headers.ContainsKey("Token"))
                {
                    var inTkn = ctx.Request.Headers.FirstOrDefault(_ => _.Key == "Token");
                    ctx.Request.Headers.Add("Authorization", inTkn.Value);
                }

                await next();
            })//.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll)
               //Token Generations
               .UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
               {
                   AllowInsecureHttp = true,
                   //The Path For generating the Toekn
                   TokenEndpointPath = new PathString("/oauth/token"),
                   //Setting the Token Expired Time (24 hours)
                   AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                   //MyAuthorizationServerProvider class will validate the user credentials
                   //Provider = new MyAuthorizationServerProvider()
                   Provider = new MyOAuthAuthorizationServerProvider()
               }).UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            app.UseSwaggerUi3(typeof(Startup).Assembly, settings =>
            {
                //針對RPC-Style WebAPI，指定路由包含Action名稱
                settings.GeneratorSettings.DefaultUrlTemplate =
                    "api/{controller}/{action}/{id?}";
                //可加入客製化調整邏輯
                settings.PostProcess = document =>
                {
                    document.Info.Title = "WebAPI 範例";
                };
                //加入Api Key定義
                settings.GeneratorSettings.DocumentProcessors.Add(new SecurityDefinitionAppender("Authorization", new OpenApiSecurityScheme()
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    Description = "Copy 'Bearer ' + valid  token into field",
                    In = OpenApiSecurityApiKeyLocation.Header
                }));
                //REF: https://github.com/RicoSuter/NSwag/issues/1304
                settings.GeneratorSettings.OperationProcessors.Add(new OperationSecurityScopeProcessor("Authorization"));

            });
        }
    }


    public class MyOAuthAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("Username", context.UserName));
            identity.AddClaim(new Claim("Password", context.Password));
            context.Validated(identity);
        }
    }
}

/*
var client = new RestClient("http://localhost:10865//oauth/token");
client.Timeout = -1;
var request = new RestRequest(Method.GET);
request.AddHeader("Content-Type", "text/plain");
request.AddParameter("text/plain", "grant_type=password&username=yourusername&password=yourpassword",  ParameterType.RequestBody);
IRestResponse response = client.Execute(request);
Console.WriteLine(response.Content);

var client = new RestClient("http://localhost:10865/api/values/");
client.Timeout = -1;
var request = new RestRequest(Method.GET);
request.AddHeader("Authorization", "Bearer F_SjclBowiFglr-U2W9wA0mEaCddFzhFloLQ8ofKwOKb5J1c8YWfL7_mYCmUOVaFKJQI8-eOmUAFOd_ASy-Ur2xmTML32Z-RSUVAN1PQLQ4Jfs35xATa0-w3bcKPCk8Ugu7_xRGuGVV0HvLIxlw1yvjI-twJPft7jFgMyrV3q77h42xqh1rvdW_bZb1rVPLb253H1g12UlyNfT5Ts2KK1BG6FXcbYQWp10CdOnvWX7naHghJKCBWRQWSrL3o7wvO");
IRestResponse response = client.Execute(request);
Console.WriteLine(response.Content);
*/