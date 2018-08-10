using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using WebApiFutLunes.Data.Contexts;
using WebApiFutLunes.Data.DTOs;
using WebApiFutLunes.Data.Repositories;
using WebApiFutLunes.Models.Player;

namespace WebApiFutLunes.Controllers
{
    public class PlayersController : ApiController
    {
        private ApplicationDbContext _context { get; set; }
        private PlayersRepository _repo { get; set; }

        public PlayersController()
        {
            _context = new ApplicationDbContext();
            _repo = new PlayersRepository(_context);
        }

        public async Task<IHttpActionResult> Get()
        {
            List<PlayerModel> players =
                Mapper.Map<List<PlayerDto>, List<PlayerModel>>(await _repo.GetAllPlayers());

            if (players == null)
            {
                return NotFound();
            }
            return Ok(players);
        }
    }
}
