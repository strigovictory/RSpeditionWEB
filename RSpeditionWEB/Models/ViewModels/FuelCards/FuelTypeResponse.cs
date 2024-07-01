namespace RSpeditionWEB.Models.ViewModels.FuelCards;

public class FuelTypeResponse : IId
{
    [JsonInclude]
    public int Id { get; set; }

    [JsonInclude]
    public string Name { get; set; }

    public override string ToString()
    {
        return $"Тип услуги «{Name ?? string.Empty}»";
    }
}
