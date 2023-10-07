using GHSpar.SocketServer.Models;
using Newtonsoft.Json;

namespace GHSpar.SocketServer.Services
{
    public class RequestHelper
    {
        public static async Task<SocketResponse?> ProcessData(string request)
        {
            try
            {
                string? result = string.Empty;
                if (request == null) { return null!; }
                var deserialized = JsonConvert.DeserializeObject<SocketRequest>(request);
                switch (deserialized?.Event)
                {
                    case "joingame":
                        result = await ApiClient.CreateOrJoinGame(deserialized.Payload);
                        return new SocketResponse()
                        {
                            Event = deserialized.Event,
                            ApiResponse = result!
                        };
                    case "playcard":
                        result = await ApiClient.RecordPlayedCard(deserialized.Payload);
                        return new SocketResponse()
                        {
                            Event = deserialized.Event,
                            ApiResponse = result!
                        };
                    default: return null!;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}