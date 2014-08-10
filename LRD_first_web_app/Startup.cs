using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LRD_first_web_app.Startup))]
namespace LRD_first_web_app
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
