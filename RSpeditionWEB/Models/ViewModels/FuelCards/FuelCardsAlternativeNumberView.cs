namespace RSpeditionWEB.Models.ViewModels.FuelCards;

public class FuelCardsAlternativeNumberRequest : EntityModifiedResponse, IComparable
{
    [JsonInclude]
    public int Id { get; set; }

    [JsonInclude]
    [CustomDisplayAttribute("Альтернативный номер", "Укажите альтернативный номер топливной карты")]
    public string Number { get; set; }

    [JsonInclude]
    public int CardId { get; set; }

    public override string ToString()
    {
        return $"Альтернативный номер заправочной карты «{Number ?? string.Empty}»";
    }

    public int CompareTo(object obj)
    {
        return obj is FuelCardsAlternativeNumberRequest item
            && item.Id == Id
            && item.Number == Number
            && item.CardId == CardId
            ? 0
            : -1;
    }
}
