using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tuto4.Startup))]
namespace Tuto4
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
