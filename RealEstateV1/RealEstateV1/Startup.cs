using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RealEstateV1.Startup))]
namespace RealEstateV1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
