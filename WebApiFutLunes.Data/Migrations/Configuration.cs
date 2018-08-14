using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebApiFutLunes.Data.Contexts;
using WebApiFutLunes.Data.DTOs;
using WebApiFutLunes.Data.Helpers;

namespace WebApiFutLunes.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApiFutLunes.Data.Contexts.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebApiFutLunes.Data.Contexts.ApplicationDbContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            manager.CreateAdmin();
            roleManager.CreateRoles();

            manager.ElevateAdmin();

            manager.CreatePlayers();
        }
    }
}
