using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Lab4_EliasHaloun.Startup))]
namespace Lab4_EliasHaloun
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
