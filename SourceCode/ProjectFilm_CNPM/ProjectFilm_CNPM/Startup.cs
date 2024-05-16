using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectFilm_CNPM.Startup))]
namespace ProjectFilm_CNPM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
