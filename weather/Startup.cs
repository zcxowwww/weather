using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(weather.Startup))]
namespace weather
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
