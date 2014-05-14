using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NwdTemplate.Startup))]
namespace NwdTemplate
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
