namespace GHSpar.Models.Db
{
    public class OTPModel
    {
        public long Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string Otp { get; set; } = string.Empty;
        public string Msisdn { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime DateApproved { get; set; }
        public OTPStatus Active { get; set; }
    }

    public enum OTPStatus
    {
        ACTIVE, USED
    }
}