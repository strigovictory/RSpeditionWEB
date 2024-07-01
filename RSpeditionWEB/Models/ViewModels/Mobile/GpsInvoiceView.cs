namespace RSpeditionWEB.Models.ViewModels;

public class GpsInvoiceView : ICloneable
{
    public GpsInvoiceView()
    {
        this.Fguid = Guid.NewGuid();
    }

    public Guid Fguid { get; set; }

    [CustomDisplayAttribute("Номер накладной", "Укажите номер накладной")]
    public string InvoiceNumber { get; set; }

    [CustomDisplayAttribute("Дата накладной", "Выберите дату накладной")]
    public DateTime InvoiceDate { get; set; }

    [CustomDisplayAttribute("Общая сумма", "Укажите общую сумму по накладной")]
    public decimal? SummaryCost { get; set; }

    public virtual int? FkTrackerModelId { get; set; }

    public string ModelName { get; set; }

    public virtual int? FkDivisionId { get; set; }

    [CustomDisplayAttribute("Подразделение", "Выберите подразделение")]
    public string DivisionName { get; set; }

    public override string ToString()
        => $"Накладная №{this.InvoiceNumber ?? "б/н"} " +
           $"от {this.InvoiceDate.FormatingDate() ?? String.Empty}г. " +
           $"на сумму {this.SummaryCost?.ToString() ?? "0"}";

    public object Clone()
    => (object)new GpsInvoiceView
    {
        Fguid = Fguid,
        InvoiceNumber = InvoiceNumber,
        InvoiceDate = InvoiceDate,
        SummaryCost = SummaryCost,
        FkTrackerModelId = FkTrackerModelId,
        ModelName = ModelName,
        FkDivisionId = FkDivisionId,
        DivisionName = DivisionName
    };
}
