namespace RSpeditionWEB.Models.ViewModels.FuelCards;

[Display(Name = "Провайдер")]
public class FuelProviderResponse : IId
{
    [JsonInclude]
    public int Id { get; set; }

    [JsonInclude]
    [CustomDisplayAttribute("Провайдер", " Укажите наименование провайдера")]
    public string Name { get; set; }

    [JsonInclude]
    public int? CountryId { get; set; }

    public override string ToString()
    {
        return $"Провайдер «{Name ?? string.Empty}»";
    }
}
