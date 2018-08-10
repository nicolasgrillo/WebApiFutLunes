using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using AutoMapper;
using WebApiFutLunes.Data.Contexts;
using WebApiFutLunes.Data.DTOs;
using WebApiFutLunes.Data.Repositories.Interface;

namespace WebApiFutLunes.Data.Repositories
{

    public class PlayersRepository : IPlayersRepository
    {
        private ApplicationDbContext _context;

        public PlayersRepository()
        {
            _context = new ApplicationDbContext();
        }

        public async Task<List<PlayerDto>> GetAllPlayers()
        {
            var users = await _context.Users.ToListAsync();
            return Mapper.Map<List<ApplicationUser>, List<PlayerDto>>(users);
        }

        public async Task<PlayerDto> GetPlayerByUsername(string username)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == username);
            return Mapper.Map<ApplicationUser, PlayerDto>(user);
        }

        public async Task<ApplicationUser> GetUserByUsername(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.UserName == username);
        }
    }
}
