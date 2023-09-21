using GHSpar.Models.Db;

namespace GHSpar.Abstractions
{
    public interface IGameService
    {
        Task<GameMatch> CreateMatch(GameMatch gameMatch, GameMatchDetail matchDetail);
        Task<GameMatchDetail> GetMatch(long matchId);
    }
}