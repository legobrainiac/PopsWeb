using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PopsWeb.Startup))]
namespace PopsWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
