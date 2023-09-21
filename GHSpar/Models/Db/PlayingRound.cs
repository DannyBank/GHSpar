namespace GHSpar.Models.Db
{
    public class PlayingRound
    {
        public long Id { get; set; }
        public int Round { get; set; }
        public long MatchId { get; set; }
        public DateTime DateRecorded { get; set; }
        public long Winner { get; set; }
    }
}