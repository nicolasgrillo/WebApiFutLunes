using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using WebApiFutLunes.Data.Contexts;
using WebApiFutLunes.Data.DTOs;
using WebApiFutLunes.Data.Repositories.Interface;
using WebApiFutLunes.Models.Player;

namespace WebApiFutLunes.Controllers
{
    public class PlayersController : ApiController
    {
        private ApplicationDbContext _context { get; set; }
        private IPlayersRepository _repo { get; set; }

        public PlayersController(IPlayersRepository repo)
        {
            _repo = repo;
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
