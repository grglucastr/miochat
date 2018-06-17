using Owin;
using Microsoft.Owin.Cors;

namespace SignalRSelfHost
{
    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }
}
