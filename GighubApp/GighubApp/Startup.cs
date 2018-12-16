using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GighubApp.Startup))]
namespace GighubApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
