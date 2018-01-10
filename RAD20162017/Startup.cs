using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RAD20162017.Startup))]
namespace RAD20162017
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
