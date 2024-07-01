namespace RSpeditionWEB.Models.ViewModels.Banks;

[Display(Name = "Банк")]
public  class BankView : EntityModifiedResponse, IId
{
    public BankView()
    {  }

    [CustomDisplayAttribute("Идентификатор банка", "Задайте идентификатор банка")]
    public int Id { get; set; }

    [CustomDisplayAttribute("Сокращенное наименование банка", "Укажите сокращенное наименование банка")]
    public string BankNameShort { get; set; }

    [CustomDisplayAttribute("Полное наименование банка", "Укажите полное наименование банка")]
    public string BankName { get; set; }

    [CustomDisplayAttribute("Полное наименование банка на англ.", "Укажите полное наименование банка на англ.")]
    public string BankNameEng { get; set; }

    [CustomDisplayAttribute("Самое сокращенное наименование банка", "Укажите самое сокращенное наименование банка")]
    public string BankNameShortest { get; set; }

    [CustomDisplayAttribute("Адрес банка", "Укажите адрес банка")]
    public string BankAddress { get; set; }

    [CustomDisplayAttribute("Адрес банка на англ.", "Укажите адрес банка на англ.")]
    public string BankAddressEng { get; set; }
}
