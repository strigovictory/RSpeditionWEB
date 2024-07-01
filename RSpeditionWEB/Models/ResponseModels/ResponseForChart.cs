namespace RSpeditionWEB.Models.Results
{
    public class ResponseForChart<T>
    {
        [JsonInclude]
        [JsonPropertyName(nameof(Names))]
        public List<string> Names { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(Values))]
        public List<T> Values { get; set; }
    }
}
