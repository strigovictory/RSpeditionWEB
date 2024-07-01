namespace RSpeditionWEB.Models.ViewModels.FuelCards;

public class FuelCardShortModel : IComparable
{
    [JsonInclude]
    public int Id { get; set; }

    [JsonInclude]
    public string Number { get; set; } = string.Empty;

    [JsonInclude]
    public string FullNumber { get; set; }

    public int CompareTo(object obj)
    {
        return (obj is FuelCardShortModel source)
            && source.Id == Id
            && source.Number == Number ? 0 : -1;
    }

    public string ToStringShort() => Number ?? string.Empty;

    public override string ToString()
    {
        return string.IsNullOrEmpty(FullNumber)
        ? $"Заправочная карта «{Number ?? string.Empty}»"
        : $"Заправочная карта «{FullNumber ?? string.Empty} / {Number ?? string.Empty}»";
    }
}