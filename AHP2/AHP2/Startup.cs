using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AHP2.Startup))]
namespace AHP2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
