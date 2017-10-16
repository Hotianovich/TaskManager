using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SMTP.WebUI.Startup))]
namespace SMTP.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
