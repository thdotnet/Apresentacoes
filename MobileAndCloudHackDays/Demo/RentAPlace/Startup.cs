using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RentAPlace.Startup))]
namespace RentAPlace
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
