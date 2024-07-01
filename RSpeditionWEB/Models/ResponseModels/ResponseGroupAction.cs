namespace RSpeditionWEB.Models.Results
{
    public class ResponseGroupAction<T, V> : ResponseBase where T : IEnumerable where V : IEnumerable
    {
        // Коллекция экземпляров, над которыми операция была успешно произведена
        [JsonInclude]
        [JsonPropertyName(nameof(SuccessItems))]
        public T SuccessItems { get; set; }

        // Коллекция-словарь экземпляров, над которыми операция не была произведена
        [JsonInclude]
        [JsonPropertyName(nameof(NotSuccessItems))]
        public V NotSuccessItems { get; set; }
    }
}
