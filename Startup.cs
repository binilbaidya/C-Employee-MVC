using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CRUDEmployeeMVC.Startup))]
namespace CRUDEmployeeMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
