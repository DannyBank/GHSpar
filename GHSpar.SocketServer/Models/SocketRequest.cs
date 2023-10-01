namespace GHSpar.SocketServer.Models
{
    public class SocketRequest
    {
        public string Event { get; set; } = null!;
        public object Payload { get; set; } = null!;
        public string Client { get; set; } = null!;
    }
}
