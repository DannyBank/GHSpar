using GHSpar.Models;
using GHSpar.Models.Db;

namespace GHSpar.Abstractions
{
    public interface IGameService
    {
        Task<GameMatch> CreateMatch(GameMatch gameMatch, GameMatchDetail matchDetail);
        Task<GameMatch> GetMatch(long matchId);
        Task<List<GameMatch>> GetMatchByPlayers(int playercount);
    }
}