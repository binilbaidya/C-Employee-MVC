using Autofac;
using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(CRUDEmployeeMVC.Startup))]
namespace CRUDEmployeeMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            var container = builder.Build();
            app.UseAutofacMiddleware(container);
            ConfigureAuth(app);
        }
    }
}
