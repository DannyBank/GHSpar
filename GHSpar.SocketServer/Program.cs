using WebSocketSharp;
using WebSocketSharp.Server;

namespace GHSpar.SocketServer
{
    public class Echo: WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            Console.WriteLine($"Received message from client {e.Data}");
            Send(e.Data);
        }
    }

    public class EchoAll: WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            Console.WriteLine($"Received message from EchoAll client {e.Data}");
            Sessions.Broadcast(e.Data);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            WebSocketServer server = new ("ws://192.168.8.118:26001");
            server.AddWebSocketService<EchoAll>("/EchoAll");
            server.Start();
            Console.WriteLine("Server started on ws://192.168.8.118:26001/EchoAll");
            Console.ReadKey();
            server.Stop();
        }
    }
}