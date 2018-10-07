using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FoodFinderIreland.Startup))]
namespace FoodFinderIreland
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
