using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LMS_MiniProject.Startup))]
namespace LMS_MiniProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
