namespace GHSpar.SocketServer.Models
{
    public class GameRequest
    {
        public long PlayerId { get; set; }
        public string PlayerName { get; set; } = null!;
        public int PlayerCount { get; set; }
    }
}
