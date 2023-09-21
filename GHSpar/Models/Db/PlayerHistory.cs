namespace GHSpar.Models.Db
{
    public class PlayerHistory
    {
        public long Id { get; set; }
        public long PlayerId { get; set; }
        public string PlayerName { get; set; } = string.Empty;
        public decimal SparCoin { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
