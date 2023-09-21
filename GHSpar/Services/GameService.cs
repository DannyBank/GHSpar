using GHSpar.Abstractions;
using GHSpar.Models.Db;

namespace GHSpar.Services
{
    public class GameService : IGameService
    {
        private readonly IDbServiceHelper _dbSvc;

        public GameService(IDbServiceHelper dbSvc)
        {
            _dbSvc = dbSvc;
        }

        public async Task<GameMatch> CreateMatch(GameMatch gameMatch, GameMatchDetail matchDetail)
        {
            return await _dbSvc.CreateMatch(matchDetail.PlayerId, matchDetail.PlayerName, gameMatch.DateCreated, 
                matchDetail.DateJoined, gameMatch.RequiredPlayers, matchDetail.Amount);
        }

        public async Task<GameMatchDetail> GetMatch(long matchId)
        {
            return await _dbSvc.GetMatchDetailByMatch(matchId);
        }
    }
}
