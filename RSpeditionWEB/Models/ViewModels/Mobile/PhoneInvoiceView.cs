namespace RSpeditionWEB.Models.ViewModels.Mobile;

[Table("tPhoneInvoices", Schema = "dbo")]
[CustomDisplayAttribute("Счет на телефонный аппарат")]
public partial class PhoneInvoiceView : IId
{
    [NotMapped]
    public int Id { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(Fguid))]
    [CustomDisplayAttribute("Идентификационный номер", "Укажите идентификационный номер")]
    public Guid Fguid { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(InvoiceNumber))]
    [CustomDisplayAttribute("Номер", "Укажите номер")]
    public string InvoiceNumber { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(InvoiceDate))]
    [CustomDisplayAttribute("Дата", "Укажите дату")]
    public DateTime InvoiceDate { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(DivisionId))]
    [CustomDisplayAttribute("Подразделение", "Выберите подразделение")]
    [CustomForeignAttribute(typeof(DivisionResponse), "Name")]
    public int DivisionId { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(SummaryCost))]
    [CustomDisplayAttribute("Общая сумма", "Укажите общую сумму")]
    public decimal? SummaryCost { get; set; }
}
