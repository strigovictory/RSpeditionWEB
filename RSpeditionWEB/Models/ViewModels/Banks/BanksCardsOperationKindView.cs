namespace RSpeditionWEB.Models.ViewModels.Banks;

public class BanksCardsOperationKindView : IId
{
    [CustomDisplayAttribute("Идентификатор операции", "Задайте идентификатор операции")]
    public int Id { get; set; }

    [CustomDisplayAttribute("Наименование разновидности операции", "Укажите наименование разновидности операции")]
    public string KindName { get; set; }
}
