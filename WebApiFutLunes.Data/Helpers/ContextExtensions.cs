using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebApiFutLunes.Data.Contexts;
using WebApiFutLunes.Data.DTOs;
using WebApiFutLunes.Data.Entities;

namespace WebApiFutLunes.Data.Helpers
{
    public static class ContextExtensions
    {
        public static void CreateAdmin(this UserManager<ApplicationUser> manager)
        {
            var user = new ApplicationUser()
            {
                UserName = "admin",
                Email = "adming@futlunes.com",
                EmailConfirmed = true,
                FirstName = "admin",
                LastName = "futlunes"
            };

            manager.Create(user, "fut@LUNES123");
        }

        public static void CreateRoles(this RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }
        }

        public static void ElevateAdmin(this UserManager<ApplicationUser> manager)
        {
            var adminUser = manager.FindByName("admin");
            manager.AddToRoles(adminUser.Id, new string[] { "Admin" });
        }

        public static void CreateDummyMatch(this UserManager<ApplicationUser> manager, ApplicationDbContext context)
        {
            Match m = new Match()
            {
                LocationMapUrl = "DummyMapUrl.com",
                LocationTitle = "DummyLocationTitle",
                MatchDate = DateTime.Now.AddDays(7),
                PlayerLimit = 10,
                Players = new List<UserSubscription>()
            };

            for (int i = 1; i < 11; i++)
            {
                var customName = "Player" + i;
                var user = new ApplicationUser()
                {
                    UserName = customName,
                    Email = customName + "@futlunes.com",
                    EmailConfirmed = false,
                    FirstName = customName,
                    LastName = "Dummy"
                };

                manager.Create(user, customName + "@123");

                var createdUser = manager.FindByName(customName);
                manager.AddToRoles(createdUser.Id, new string[] { "User" });

                var us = new UserSubscription(createdUser, DateTime.Now);

                m.Players.Add(us);
            }

            context.Matches.Add(m);
        }
    }
}
