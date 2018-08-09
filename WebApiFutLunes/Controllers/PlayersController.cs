using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using WebApiFutLunes.Data.Contexts;
using WebApiFutLunes.Data.DTOs;
using WebApiFutLunes.Models.Player;

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
            List<PlayerModel> players = 
                Mapper.Map<List<ApplicationUser>, List<PlayerModel>>(await _context.Users.ToListAsync());

            if (players == null)
            {
                return NotFound();
            }
            return Ok(players);
        }
    }
}
