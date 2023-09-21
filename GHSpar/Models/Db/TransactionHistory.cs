namespace GHSpar.Models.Db
{
    public class TransactionHistory
    {
        public int Id { get; set; }
        public DateTime EntryDate { get; set; }
        public string TransactionId { get; set; } = string.Empty;
        public long PlayerId { get; set; }
        public string PlayerName { get; set; } = string.Empty;
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public decimal Fee { get; set; }
    }
}
