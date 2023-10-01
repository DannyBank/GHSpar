using GHSpar.SocketServer.Models;
using Newtonsoft.Json;
using System.Text;

namespace GHSpar.SocketServer.Services
{
    public class RequestHelper
    {


        public static async Task<SocketResponse?> ProcessData(string request)
        {
            try
            {
                if (request == null) { return null!; }
                var deserialized = JsonConvert.DeserializeObject<SocketRequest>(request);
                switch (deserialized?.Event)
                {
                    case "joingame":
                        var result = await JoinGame(deserialized.Payload);
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

        private static async Task<string?> JoinGame(object payload)
        {
            var serialized = JsonConvert.SerializeObject(payload);
            var gameRequest = JsonConvert.DeserializeObject<GameRequest>(serialized);
            if (gameRequest is null) { return null; }

            var httpClient = new HttpClient() { BaseAddress = new Uri("http://192.168.8.118:25001/api/")};
            var httpOut = await httpClient.GetAsync(new Uri($"game/create/match/{gameRequest.PlayerCount}/{gameRequest.PlayerId}", UriKind.Relative));
            if (!httpOut.IsSuccessStatusCode)
                return null;

            return await httpOut.Content.ReadAsStringAsync();
        }
    }
}