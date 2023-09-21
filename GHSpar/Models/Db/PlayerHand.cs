namespace GHSpar.Models.Db
{
    public class PlayerHand
    {
        public long MatchId { get; set; }
        public long PlayerId { get; set; }
        public HashSet<Card> Hands { get; set; } = null!;
    }
}