using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebApiFutLunes.Data.Entities;
using WebApiFutLunes.Data.DTOs;

namespace WebApiFutLunes.Data.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("FutLunesDb", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Match> Matches { get; set; }
    }
}
