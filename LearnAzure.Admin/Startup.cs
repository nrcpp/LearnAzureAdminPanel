using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LearnAzure.Admin.Startup))]
namespace LearnAzure.Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
