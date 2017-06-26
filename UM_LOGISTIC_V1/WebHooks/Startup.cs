using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(UM_LOGISTIC_V1.WebHooks.Startup))]

namespace UM_LOGISTIC_V1.WebHooks
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
