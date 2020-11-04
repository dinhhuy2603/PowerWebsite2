using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PowerManagement.Startup))]
namespace PowerManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
