using System.Text.Json.Serialization;

namespace Contracts.Dtos
{
    public class AggregateBarDto
    {
        [JsonPropertyName("c")]
        public float ClosePrice { get; set; }

        [JsonPropertyName("h")]
        public float HighestPrice { get; set; }

        [JsonPropertyName("l")]
        public float LowestPrice { get; set; }

        [JsonPropertyName("n")]
        public long NumberOfTransactions { get; set; }

        [JsonPropertyName("o")]
        public float OpenPrice { get; set; }

        [JsonPropertyName("t")]
        public double StartTimestamp { get; set; }

        [JsonPropertyName("v")]
        public decimal TradingVolume { get; set; }

        [JsonPropertyName("vw")]
        public float VolumeWeighted { get; set; }
    }

}
