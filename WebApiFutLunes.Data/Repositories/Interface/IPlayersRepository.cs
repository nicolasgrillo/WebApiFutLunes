using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiFutLunes.Data.DTOs;

namespace WebApiFutLunes.Data.Repositories.Interface
{
    public interface IPlayersRepository
    {
        Task<List<PlayerDto>> GetAllPlayers();
        Task<PlayerDto> GetPlayerByUsername(string username);
        Task<ApplicationUser> GetUserByUsername(string username);
    }
}