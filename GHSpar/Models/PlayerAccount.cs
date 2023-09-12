namespace GHSpar.Models
{
    public class PlayerAccount
    {
        public long Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Msisdn { get; set; } = string.Empty;
        public string Pin { get; set; } = string.Empty;
        public PlayerStatus Status { get; set; }
    }
}