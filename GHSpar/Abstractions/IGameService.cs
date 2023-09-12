using GHSpar.Models;

namespace GHSpar.Abstractions
{
    public interface IGameService
    {
        Task<GameMatch> CreateMatch(int players, long playerId);
        Task<GameMatch> GetMatch(long matchId);
    }
}