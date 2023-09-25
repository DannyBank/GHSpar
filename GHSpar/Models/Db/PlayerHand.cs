using Newtonsoft.Json;

namespace GHSpar.Models.Db
{
    public class PlayerHand
    {
        [JsonProperty(nameof(MatchId))]
        public long MatchId { get; set; }

        [JsonProperty(nameof(PlayerId))]
        public long PlayerId { get; set; }

        [JsonProperty(nameof(Hands))]
        public List<Card> Hands { get; set; } = null!;
    }
}