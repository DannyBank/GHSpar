namespace GHSpar.Models
{
    public class PlayingRound
    {
        public long Id { get; set; }
        public long MatchId { get; set; }
        public DateTime DateRecorded { get; set; }
        public long Winner { get; set; }
        public string PlayerName { get; set; } = string.Empty;
    }
}