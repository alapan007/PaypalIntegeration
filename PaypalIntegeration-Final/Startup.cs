using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PaypalIntegeration.Startup))]
namespace PaypalIntegeration
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
