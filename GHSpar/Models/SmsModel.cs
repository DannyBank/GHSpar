namespace GHSpar.Models
{
    public class SmsModel
    {
        public long Id { get; set; }
        public string Msisdn { get; set; } = string.Empty;
        public string Origin { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}