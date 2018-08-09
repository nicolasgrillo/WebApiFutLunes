using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiFutLunes.Data.Contexts;

namespace WebApiFutLunes.Controllers
{
    public class PlayersController : ApiController
    {
        private ApplicationDbContext _context { get; set; }

        public PlayersController()
        {
            _context = new ApplicationDbContext();
        }

        public async Task<IHttpActionResult> Get()
        {
            var players = await _context.Users.ToListAsync();
            if (players == null)
            {
                return NotFound();
            }
            return Ok(players);
        }
    }
}
