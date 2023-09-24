using GHSpar.Abstractions;
using GHSpar.Models;
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
            var gameData = await _dbSvc.CreateMatch(matchDetail.PlayerId, matchDetail.PlayerName, gameMatch.DateCreated, 
                                                    matchDetail.DateJoined, gameMatch.RequiredPlayers, matchDetail.Amount);
            var combinedGameMatches = gameData
            .GroupBy(match => new
            {
                match.Id,
                match.DateCreated,
                match.RequiredPlayers,
                match.CurrentPlayers,
                match.Active
            })
            .Select(group => new GameMatch
            {
                Id = group.Key.Id,
                DateCreated = group.Key.DateCreated,
                RequiredPlayers = group.Key.RequiredPlayers,
                CurrentPlayers = group.Key.CurrentPlayers,
                Active = group.Key.Active,
                MatchDetails = new HashSet<GameMatchDetail>(group.Select(match => new GameMatchDetail
                {
                    DetailId = match.DetailId,
                    MatchId = match.MatchId,
                    PlayerId = match.PlayerId,
                    PlayerName = match.PlayerName,
                    Amount = match.Amount,
                    DateJoined = match.DateJoined
                }))
            })
            .ToList();
            return combinedGameMatches.FirstOrDefault()!;
        }

        public async Task<GameMatch> GetMatch(long matchId)
        {
            return await _dbSvc.GetMatchDetailByMatch(matchId);
        }
    }
}