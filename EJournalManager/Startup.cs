using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EJournalManager.Startup))]
namespace EJournalManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
