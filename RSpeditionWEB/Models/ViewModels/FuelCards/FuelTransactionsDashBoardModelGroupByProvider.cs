namespace RSpeditionWEB.Models.ViewModels.FuelCards;

public class FuelTransactionsDashBoardModelGroupByProvider
{
    [CustomDisplayAttribute("Провайдер", "")]
    public string ProviderName { get; set; } // наименование провайдера

    public FuelTransactionsDashBoardModel TransactionsInfoByProvider { get; set; }

    public List<FuelTransactionsDashBoardModel_GroupBy_Division> TransactionsInfoByDivision { get; set; }
}

public class FuelTransactionsDashBoardModel_GroupBy_Division
{
    [CustomDisplayAttribute("Подразделение", "")]
    public string DivisionName { get; set; } // наименование подразделения

    public FuelTransactionsDashBoardModel TransactionsByProviderByDivision { get; set; } 
}

public class FuelTransactionsDashBoardModel
{
    [CustomDisplayAttribute("Первая в периоде", "")]
    public string StartUploadName => StartUpload?.ToString("dd.MM.yyyy") ?? "-//-";

    public DateTime? StartUpload { get; set; } // начальная дата загрузки тр-ций внутри выбранного периода

    [CustomDisplayAttribute("Последняя в периоде", "")]
    public string FinishUploadName => FinishUpload?.ToString("dd.MM.yyyy") ?? "-//-";

    public DateTime? FinishUpload { get; set; } // окончание периода загрузки тр-ций внутри выбранного периода

    [CustomDisplayAttribute("По всем типам отчетов", "")]
    public int NumberTotal => NumberNotSetReportKind + NumberDayly + NumberMonthly; // общее число загруженных транзакций

    [CustomDisplayAttribute("Тип не определен", "")]
    public int NumberNotSetReportKind { get; set; } // число загруженных транзакций, в которых тип отчета не проставлен

    [CustomDisplayAttribute("Ежедневный", "")]
    public int NumberDayly { get; set; } // число загруженных транзакций из ежедневного отчета

    [CustomDisplayAttribute("Ежемесячный", "")]
    public int NumberMonthly { get; set; } // число загруженных транзакций из ежемесячного отчета

    [CustomDisplayAttribute("Проверена", "")]
    public int NumberChecked { get; set; } // число загруженных транзакций с пометкой «Проверено»
}