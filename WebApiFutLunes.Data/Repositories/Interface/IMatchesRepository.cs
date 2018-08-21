using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiFutLunes.Data.DTOs;
using WebApiFutLunes.Data.Entities;

namespace WebApiFutLunes.Data.Repositories.Interface
{
    public interface IMatchesRepository
    {
        Task<List<Match>> GetAllMatchesAsync();
        Task<Match> GetMatchByIdAsync(int id);
        Task<Match> GetCurrentMatchAsync();
        Task<int> AddMatchAsync(Match match);
        Task<int> SignUpPlayerAsync(Match match, UserSubscription us);
        Task<int> DismissPlayerAsync(Match match, UserSubscription us);
        Task<int> UpdateMatchAsync(Match match, AddUpdateMatchDto aumDto);
        Task<int> ConfirmMatchAsync(Match match);
    }
}