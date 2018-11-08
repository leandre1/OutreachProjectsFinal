using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Outreach.Web.Startup))]
namespace Outreach.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
