namespace RSpeditionWEB.Models.ViewModels.Mobile
{
    [Display(Name = "Модель")]
    public class GpstrackerModelView
    {
        [JsonInclude]
        [JsonPropertyName(nameof(Id))]
        public int Id { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(ModelName))]
        [CustomDisplayAttribute("Модель", "Выберите модель")]
        public string ModelName { get; set; }
    }
}
