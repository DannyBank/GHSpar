namespace GHSpar.SocketServer.Models
{
    public class GameMatch
    {
        public long Id { get; set; }
        public DateTime DateCreated { get; set; }
        public int RequiredPlayers { get; set; }
        public int CurrentPlayers { get; set; }
        public bool Active { get; set; }
        public HashSet<GameMatchDetail> MatchDetails { get; set; }
    }

    public class GameMatchDetail
    {
        public long DetailId { get; set; }
        public long MatchId { get; set; }
        public long PlayerId { get; set; }
        public string PlayerName { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime DateJoined { get; set; }
    }
}