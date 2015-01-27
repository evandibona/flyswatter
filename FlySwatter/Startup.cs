using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FlySwatter.Startup))]
namespace FlySwatter
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
