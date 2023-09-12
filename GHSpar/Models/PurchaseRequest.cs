namespace GHSpar.Models
{
    public class PurchaseRequest
    {
        public long Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public long PlayerId { get; set; }
        public long PlayerName { get; set; }
        public decimal AmountCoins { get; set; }
        public decimal AmountGHS { get; set; }
        public decimal AmountUSD { get; set; }
    }

    public class PurchaseResponse
    {
        public string Status { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; }
        public object Data { get; set; } = new();
    }
}