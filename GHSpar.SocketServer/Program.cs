using GHSpar.SocketServer.Models;
using GHSpar.SocketServer.Services;
using Newtonsoft.Json;
using PHS.Networking.Server.Events.Args;
using System.Net.WebSockets;
using System.Text;
using WebsocketsSimple.Server;
using WebsocketsSimple.Server.Events.Args;
using WebsocketsSimple.Server.Models;

namespace GHSpar.SocketServer
{
    internal class Program
    {
        static IWebsocketServer server = null!;

        static void Main(string[] args)
        {
            try
            {
                var message = new SocketResponse { ApiResponse = null!, Event = "connecting" };
                server = new WebsocketServer(new ParamsWSServer(26001, JsonConvert.SerializeObject(message), pingIntervalSec: 20));
                server.MessageEvent += OnMessageEvent;
                server.ConnectionEvent += OnConnectionEvent;
                server.ErrorEvent += OnErrorEvent;
                server.ServerEvent += OnServerEvent;                
                server.StartAsync();

                string input = Console.ReadLine()!.Trim();
                if (input == "exit")
                {
                    server.StopAsync();
                    server.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.Write($"Error occurred:\n{ex.Message}");
            }
        }

        private static void OnServerEvent(object sender, ServerEventArgs args)
        {
            Console.WriteLine($"{DateTime.Now}: OnServerEvent ({args.ServerEventType}) triggered");
        }

        private static void OnErrorEvent(object sender, WSErrorServerEventArgs args)
        {
            Console.WriteLine($"{DateTime.Now}: OnErrorEvent triggered {args.Exception.Message}");
        }

        private static void OnConnectionEvent(object sender, WSConnectionServerEventArgs args)
        {
            Console.WriteLine($"{DateTime.Now}: OnConnectionEvent ({args.ConnectionEventType}) triggered");
        }

        private static void OnMessageEvent(object sender, WSMessageServerEventArgs args)
        {
            try
            {
                if (string.IsNullOrEmpty(args.Message)) { return; }
                var result = RequestHelper.ProcessData(args.Message).GetAwaiter().GetResult();
                if (result is null) { return; }
                var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(result));
                foreach (ConnectionWSServer connection in server.Connections)
                    connection.Websocket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);

                Console.WriteLine($"{DateTime.Now}: OnMessageEvent triggered, Message: {args.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now}: OnMessageEvent error triggered: {ex.Message}");
            }
        }
    }
}