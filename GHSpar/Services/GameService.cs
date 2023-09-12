using GHSpar.Abstractions;
using GHSpar.Models;

namespace GHSpar.Services
{
    public class GameService : IGameService
    {
        public async Task<GameMatch> CreateMatch(int players, long playerId)
        {
            // go to db
            // find a [4-player, eg] match
            // if none exists, create one and return game
            return new GameMatch
            {
                DateCreated = DateTime.Now,
                DateJoined = DateTime.Now,
                MatchId = 342324238429,
                Players = new HashSet<PlayerAccount>
                {
                    new PlayerAccount
                    {
                        Id = 3424,
                        Msisdn = "233242783898",
                        Status = PlayerStatus.ACTIVE,
                        Username = "Dbank"
                    },
                    new PlayerAccount
                    {
                        Id = 3426,
                        Msisdn = "233243290123",
                        Status = PlayerStatus.ACTIVE,
                        Username = "Phresha"
                    }
                }
            };
        }

        public async Task<GameMatch> GetMatch(long matchId)
        {
            return new GameMatch
            {
                DateCreated = DateTime.Now,
                DateJoined = DateTime.Now,
                MatchId = 342324238429,
                Players = new HashSet<PlayerAccount>
                {
                    new PlayerAccount
                    {
                        Id = 3424,
                        Msisdn = "233242783898",
                        Status = PlayerStatus.ACTIVE,
                        Username = "Dbank"
                    },
                    new PlayerAccount
                    {
                        Id = 3426,
                        Msisdn = "233243290123",
                        Status = PlayerStatus.ACTIVE,
                        Username = "Phresha"
                    }
                }
            };
        }
    }
}
