namespace GHSpar.Models
{
    public class GameMatch
    {
        public long MatchId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateJoined { get; set; }
        public int RequiredPlayers { get; set; }
        public HashSet<PlayerAccount> Players { get; set; } = new();
        public HashSet<PlayerBet> PlayerBets { get; set; } = new();
    }
}