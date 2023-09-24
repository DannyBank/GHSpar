namespace GHSpar.Models
{
    public class GameMatchData
    {
        public long Id { get; set; }
        public DateTime DateCreated { get; set; }
        public int RequiredPlayers { get; set; }
        public int CurrentPlayers { get; set; }
        public bool Active { get; set; }
        public long DetailId { get; set; }
        public long MatchId { get; set; }
        public long PlayerId { get; set; }
        public string PlayerName { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime DateJoined { get; set; }
    }
}
