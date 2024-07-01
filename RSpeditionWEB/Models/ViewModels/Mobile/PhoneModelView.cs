namespace RSpeditionWEB.Models.ViewModels.Mobile;

[Table("tPhoneModels", Schema = "dbo")]
[CustomDisplayAttribute("Модель телефонного аппарата")]
public partial class PhoneModelView : IId
{
    [JsonInclude]
    [JsonPropertyName(nameof(Id))]
    [CustomDisplayAttribute("Идентификационный номер", "Укажите идентификационный номер")]
    public int Id { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(ModelName))]
    [CustomDisplayAttribute("Наименование", "Укажите наименование")]
    public string ModelName { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(ModelCostEur))]
    [CustomDisplayAttribute("Стоимость в евро", "Укажите стоимость в евро")]
    public decimal? ModelCostEur { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(ModelPicture))]
    [CustomDisplayAttribute("Изображение", "Загрузите изображение")]
    public byte[] ModelPicture { get; set; }
}
