namespace GHSpar.Models
{
    public class OTPModel
    {
        public long Id { get; set; }
        public string Number { get; set; } = string.Empty;
        public string RelatedMsisdn { get; set; } = string.Empty;
        public DateTime DateGenerated { get; set; }
        public DateTime DateApproved { get; set; }
        public OTPStatus Status { get; set; }
    }

    public enum OTPStatus
    {
        ACTIVE, USED
    }
}