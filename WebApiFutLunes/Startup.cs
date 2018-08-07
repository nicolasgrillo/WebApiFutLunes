using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WebApiFutLunes.Startup))]

namespace WebApiFutLunes
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
