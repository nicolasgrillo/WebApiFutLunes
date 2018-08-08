using Microsoft.AspNet.Identity.EntityFramework;
using WebApiFutLunes.Data.Models;

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
    }
}
