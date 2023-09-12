namespace GHSpar.Models
{
    public class CardPlay
    {
        public int Id { get; set; }
        public long PlayerId { get; set; }
        public string PlayerName { get; set; } = string.Empty;
        public long MatchId { get; set; }
        public int Round { get; set; }
        public string Suit { get; set; } = string.Empty;
        public int Rank { get; set; }
        public DateTime DatePlayed { get; set; }
    }
}