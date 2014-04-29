using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MessageBoard.Web.Display.Startup))]
namespace MessageBoard.Web.Display
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
