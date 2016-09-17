using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TheTruck.Web.Startup))]
namespace TheTruck.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
