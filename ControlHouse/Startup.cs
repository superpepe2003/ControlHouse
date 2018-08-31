using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ControlHouse.Startup))]
namespace ControlHouse
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
