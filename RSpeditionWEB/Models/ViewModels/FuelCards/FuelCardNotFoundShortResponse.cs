namespace RSpeditionWEB.Models.ViewModels.FuelCards;

public class FuelCardNotFoundShortResponse : IId
{
    [CustomDisplayAttribute("Идентификатор", "")]
    public int Id { get; set; }

    [CustomDisplayAttribute("Карт", "")]
    public string Number { get; set; }

    public override string ToString()
    {
        return $"Топливная карта с номером «{Number ?? string.Empty}» ";
    }
}
