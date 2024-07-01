namespace RSpeditionWEB.Models.ViewModels.Banks;

[Display(Name = "Операция по банковской карте")]
public class BanksCardsOperation_iew : EntityModifiedResponse, IId, ICloneable
{
    public BanksCardsOperation_iew()
    { }

    [CustomDisplayAttribute("Идентификатор операции", "Задайте идентификатор операции")]
    public int Id { get; set; }

    [CustomDisplayAttribute("Дата выписки", "Укажите дату выписки")]
    public DateTime ReportDate { get; set; }

    [CustomForeignAttribute(typeof(EmployeeView), "FullName")]
    public virtual int EmployeeId { get; set; }

    [CustomDisplayAttribute("Сотрудник", "Выберите сотрудника")]
    public string EmployeeName { get; set; }

    [CustomForeignAttribute(typeof(EmployeeCreditCardView), "Rbsnumber")]
    public virtual int EmployeeCreditCardId { get; set; }

    [CustomDisplayAttribute("Номер RBS", "Укажите номер RBS")]
    public string Rbsnumber { get; set; }

    [CustomDisplayAttribute("Дата операции", "Укажите дату операции")]
    public DateTime OperationDate { get; set; }

    [CustomDisplayAttribute("Информация по операции", "Укажите информацию по операции")]
    public string Details { get; set; }

    [CustomDisplayAttribute("Сумма операции", "Укажите сумму операции")]
    public decimal AmountOperationOriginal { get; set; } // в валюте операции

    [CustomForeignAttribute(typeof(BanksCardsOperationKindView), "KindName")]
    public virtual int KindOperationId { get; set; }

    [CustomDisplayAttribute("Вид операции", "Выберите вид операции")]
    public string KindOperationName { get; set; }

    [CustomForeignAttribute(typeof(CurrencyResponse), "Name")]
    public virtual int CurrencyOperationId { get; set; }

    [CustomDisplayAttribute("Валюта операции", "Выберите валюту операции")]
    public string CurrencyOperationName { get; set; }

    [CustomDisplayAttribute("Сумма списания с комиссией", "Укажите сумму списания с комиссией в валюте отчета")]
    public decimal AmountTotal { get; set; } // в валюте отчета

    [CustomDisplayAttribute("Комиссия", "Укажите сумму комиссии банка в валюте отчета")]
    public decimal? AmountCommission { get; set; }// в валюте отчета

    [CustomDisplayAttribute("Сумма списания без комиссии", "Укажите сумму списания без комиссии в валюте отчета")]
    public decimal AmountWithoutCommission => this.AmountTotal - (this.AmountCommission ?? 0);

    [CustomDisplayAttribute("Сумма списания без комиссии, евро", "Укажите сумму списания без комиссии в евро")]
    public decimal AmountWithoutCommissionEuro =>
                                                this.CurrencyReportId == 2 // EURO
                                                ? 
                                                this.AmountWithoutCommission
                                                :
                                                this.AmountWithoutCommission / (this.CurrencyRate * this.CurrencyRateUnit)
                                                ;

    [CustomForeignAttribute(typeof(CurrencyResponse), "Name")]
    public virtual int CurrencyReportId { get; set; }

    [CustomDisplayAttribute("Счет", "Выберите счет")]
    public string CurrencyReportName { get; set; }

    [CustomForeignAttribute(typeof(CurrencyRateView), "DateRate")]
    public virtual int? CurrencyRateId { get; set; } // если отчет-выписка банка в евро курс 1:1

    [CustomDisplayAttribute("Курс валюты на дату операции", "Выберите курс валюты на дату операции")]
    public decimal CurrencyRate { get; set; }

    [CustomDisplayAttribute("Единица курса валюты", "Укажите единицу курса валюты")]
    public int CurrencyRateUnit { get; set; }

    [CustomForeignAttribute(typeof(RideDriverReportView), "ReportDate")]
    public virtual int? RideDriverReportId { get; set; }

    [CustomDisplayAttribute("Номер рейса", "Выберите рейс")]
    public string RideDriverReportName { get; set; }

    public object Clone()
    => (object)(new BanksCardsOperation_iew
    {
        CurrencyReportId = this.CurrencyReportId, 
        CurrencyReportName = this.CurrencyReportName,
        CurrencyRate = this.CurrencyRate,
        RideDriverReportId = this.RideDriverReportId,
        RideDriverReportName = this.RideDriverReportName,
        AmountCommission = this.AmountCommission,
        AmountOperationOriginal = this.AmountOperationOriginal,   
        AmountTotal = this.AmountTotal,  
        ModifiedOn = this.ModifiedOn,
        ModifiedBy = this.ModifiedBy, 
        CurrencyOperationId = this.CurrencyOperationId,  
        CurrencyRateId = this.CurrencyRateId, 
        CurrencyRateUnit = this.CurrencyRateUnit,
        CurrencyOperationName = this.CurrencyOperationName,    
        Details = this.Details,   
        EmployeeCreditCardId = this.EmployeeCreditCardId,
        EmployeeId = this.EmployeeId,  
        EmployeeName = this.EmployeeName, 
        Id = this.Id,
        KindOperationId = this.KindOperationId,
        KindOperationName = this.KindOperationName,    
        OperationDate = this.OperationDate,   
        Rbsnumber = this.Rbsnumber,
        ReportDate = this.ReportDate
    });

    public override string ToString()
    => $"Банковская операция от {this?.OperationDate.Date.FormatingDate() ?? string.Empty}» " +
       $"на сумму {this?.AmountOperationOriginal.FormatingDecimalToString() ?? "0"} " +
       $"{(string.IsNullOrEmpty(this.CurrencyOperationName) ? string.Empty : this.CurrencyOperationName)} " +
       $"по карте с номером RBS «{(string.IsNullOrEmpty(this.Rbsnumber) ? string.Empty : this.Rbsnumber)} » " +
       $"(сотрудник {(string.IsNullOrEmpty(this.EmployeeName) ? string.Empty : this.EmployeeName)} )";
}
