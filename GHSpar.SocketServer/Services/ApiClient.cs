using GHSpar.SocketServer.Models;
using Newtonsoft.Json;
using System.Text;

namespace GHSpar.SocketServer.Services
{
    internal class ApiClient
    {


        public static async Task<string?> CreateOrJoinGame(object payload)
        {
            var serialized = JsonConvert.SerializeObject(payload);
            var gameRequest = JsonConvert.DeserializeObject<GameRequest>(serialized);
            if (gameRequest is null) { return null; }

            var httpClient = new HttpClient() { BaseAddress = new Uri("http://192.168.8.118:25001/api/") };
            var httpOut = await httpClient.GetAsync(new Uri($"game/create/match/{gameRequest.PlayerCount}/{gameRequest.PlayerId}", UriKind.Relative));
            if (!httpOut.IsSuccessStatusCode)
                return null;

            return await httpOut.Content.ReadAsStringAsync();
        }

        public static async Task<string?> RecordPlayedCard(object payload)
        {
            var serialized = JsonConvert.SerializeObject(payload);
            var cardPlay = JsonConvert.DeserializeObject<CardPlay>(serialized);
            if (cardPlay is null) { return null; }

            var content = new StringContent(JsonConvert.SerializeObject(cardPlay), Encoding.UTF8, "application/json");
            var httpClient = new HttpClient() { BaseAddress = new Uri("http://192.168.8.118:25001/api/") };
            var httpOut = await httpClient.PostAsync(new Uri($"game/play/card", UriKind.Relative), content);
            if (!httpOut.IsSuccessStatusCode)
                return null;

            return await httpOut.Content.ReadAsStringAsync();
        }
    }
}
