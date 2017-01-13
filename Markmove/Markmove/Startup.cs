using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Markmove.Startup))]
namespace Markmove
{
    using System.Data.Entity;
    using Migrations;
    using Models;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MarkmoveDbContext, Configuration>());

            this.ConfigureAuth(app);
        }
    }
}
