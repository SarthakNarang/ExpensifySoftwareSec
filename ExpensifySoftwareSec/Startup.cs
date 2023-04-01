using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ExpensifySoftwareSec.Startup))]
namespace ExpensifySoftwareSec
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
