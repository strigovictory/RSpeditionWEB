namespace RSpeditionWEB.Models.Results
{
    public class ResponseSingleAction<T> : ResponseBase where T : new()
    {
        public ResponseSingleAction() : base()
        {
            this.Item = new();
        }

        public ResponseSingleAction(string result):base(result)
        {
            this.Item = new();
        }

        public ResponseSingleAction(string result, T item) : base(result)
        {
            this.Item = item;
        }

        [JsonInclude]
        [JsonPropertyName(nameof(Item))]
        public T Item { get; set; }
    }
}
