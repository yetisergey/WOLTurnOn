using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(odmswww.WebApplication.App_Start.Startup))]
namespace odmswww.WebApplication.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}