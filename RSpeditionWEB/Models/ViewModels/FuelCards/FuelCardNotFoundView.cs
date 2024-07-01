namespace RSpeditionWEB.Models.ViewModels.FuelCards;

public class FuelCardNotFoundView : IId
{
    [CustomDisplayAttribute("Идентификатор", "")]
    public int Id { get; set; }

    [CustomDisplayAttribute("Карта", "")]
    public string Number { get; set; }

    [CustomDisplayAttribute("Пользователь", "")]
    public string UserName { get; set; }

    [CustomDisplayAttribute("Дата загрузки отчета", "")]
    public DateTime ImportDate { get; set; }

    public virtual int FuelProviderId { get; set; }

    [CustomDisplayAttribute("Провайдер", "")]
    public string FuelProviderName {get; set; }

    public override string ToString()
    {
        return $"Заправочная карта «{Number ?? string.Empty}»";
    }
}
