using System.Text.Json.Serialization;

namespace HolidayApi.Data.ExternalModels
{
    public class StateModelResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nome")]
        public required string Name { get; set; }

        [JsonPropertyName("sigla")]
        public required string StateCode { get; set; }

        [JsonPropertyName("regiao")]
        public required RegionState Region { get; set; }
    }

    public class RegionState
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nome")]
        public required string Name { get; set; }

        [JsonPropertyName("sigla")]
        public required string RegionCode { get; set; }
    }
}