using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WebApiFutLunes.Data.Contexts;
using WebApiFutLunes.Data.DTOs;
using WebApiFutLunes.Data.Entities;
using WebApiFutLunes.Data.Repositories.Interface;

namespace WebApiFutLunes.Data.Repositories
{
    public class MatchesRepository : IMatchesRepository
    {
        private ApplicationDbContext _context;

        public MatchesRepository()
        {
            _context = new ApplicationDbContext();
        }

        public async Task<List<Match>> GetAllMatchesAsync()
        {
            return await _context.Matches.ToListAsync();
        }

        public async Task<Match> GetMatchByIdAsync(int id)
        {
            return await _context.Matches.FindAsync(id);
        }

        public async Task<Match> GetCurrentMatchAsync()
        {
            var lastMatchId = _context.Matches.Max(m => m.Id);
            return await _context.Matches.FindAsync(lastMatchId);
        }

        public async Task<int> AddMatchAsync(Match match)
        {
            _context.Matches.Add(match);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> SignUpPlayerAsync(Match match, UserSubscription us)
        {
            match.Players.Add(us);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DismissPlayerAsync(Match match, UserSubscription us)
        {
            match.Players.Remove(us);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateMatchAsync(Match match, AddUpdateMatchDto aumDto)
        {
            match.MatchDate = aumDto.MatchDate;
            match.LocationMapUrl = aumDto.LocationMapUrl;
            match.LocationTitle = aumDto.LocationTitle;
            match.PlayerLimit = aumDto.PlayerLimit;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> ConfirmMatchAsync(Match match)
        {
            foreach (var userSubscription in match.Players)
            {
                var player = _context.Users.SingleOrDefault(u => u.UserName == userSubscription.User);
                if (player != null) player.Appearances++;
            }

            return await _context.SaveChangesAsync();
        }
    }
}
