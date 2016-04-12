using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SHA_Mvc_Lab01.Startup))]

namespace SHA_Mvc_Lab01
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}