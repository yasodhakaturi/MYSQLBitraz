using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Analytics.Startup))]
namespace Analytics
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
