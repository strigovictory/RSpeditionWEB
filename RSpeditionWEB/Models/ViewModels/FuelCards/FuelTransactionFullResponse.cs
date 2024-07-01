using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace RSpeditionWEB.Models.ViewModels;

[Display(Name = "Транзакция по топливной карте")]
public class FuelTransactionFullResponse : EntityModifiedResponse, IId
{
    [JsonInclude]
    [CustomDisplayAttribute("Идентификатор", "Задайте идентификатор транзакции по заправке топливом")]
    public int Id { get; set; }

    [JsonInclude]
    [CustomDisplayAttribute("Идентификатор провайдера", "Укажите идентификатор транзакции у провайдера")]
    public string TransactionID { get; set; }

    [JsonInclude]
    [Required(ErrorMessage = "Укажите дату транзакции")]
    [CustomDisplayAttribute("Дата", "Укажите дату транзакции")]
    public virtual DateTime OperationDate { get; set; }

    [JsonIgnore]
    [CustomDisplayAttribute("Дата", "Укажите дату транзакции")]
    public DateTime OperationDateOnly => OperationDate.Date;

    [JsonIgnore]
    [CustomDisplayAttribute("Время", "Укажите время транзакции")]
    public string OperationDate_Time => $"{this.OperationDate.FormatingTime()}";

    [JsonInclude]
    [CustomDisplayAttribute("Количество", "Укажите количество")]
    [Required(ErrorMessage = "Укажите количество")]
    public decimal? Quantity { get; set; }

    [JsonInclude]
    [CustomDisplayAttribute("Цена", "Укажите цену")]
    public decimal? Cost { get; set; }

    [JsonInclude]
    [CustomDisplayAttribute("Стоимость", "Укажите стоимость")]
    public decimal? TotalCost { get; set; }

    [JsonInclude]
    [CustomDisplayAttribute("Проверена", "Сделайте отметку о проверке тринзакции")]
    public bool IsCheck { get; set; }

    [JsonInclude]
    [CustomDisplayAttribute("Подразделение", "Выберите подразделение")]
    public string DivisionName { get; set; }

    [JsonInclude]
    [CustomForeignAttribute(typeof(FuelProviderResponse), nameof(FuelProviderResponse.Name))]
    [ValidateId(PropName = "Провайдер")]
    public virtual int ProviderId { get; set; }

    [JsonInclude]
    [CustomDisplayAttribute("Провайдер", "Выберите провайдера")]
    public string ProviderName { get; set; }

    [JsonInclude]
    [CustomForeignAttribute(typeof(FuelTypeResponse), nameof(FuelTypeResponse.Name))]
    [ValidateId(PropName = "Разновидность услуги")]
    public virtual int FuelTypeId { get; set; }

    [JsonInclude]
    [CustomDisplayAttribute("Услуга", "Выберите разновидность услуги/товара")]
    public string FuelTypeName { get; set; }

    [JsonInclude]
    [CustomForeignAttribute(typeof(CurrencyResponse), nameof(CurrencyResponse.Name))]
    public virtual int? CurrencyId { get; set; }

    [JsonInclude]
    [CustomDisplayAttribute("Валюта", "Укажите валюту")]
    public string CurrencyName { get; set; }

    [JsonInclude]
    [CustomForeignAttribute(typeof(FuelCardFullModel), nameof(FuelCardFullModel.Number))]
    [ValidateId(PropName = "Топливная карта")]
    public virtual int CardId { get; set; }

    [JsonInclude]
    [CustomDisplayAttribute("Карта", "Выберите топливную карту")]
    public string CardName { get; set; }

    [JsonInclude]
    [CustomForeignAttribute(typeof(CountryResponse), nameof(CountryResponse.Name))]
    public virtual int? CountryId { get; set; }

    [JsonInclude]
    [CustomDisplayAttribute("Страна", "Выберите страну, в которой была оказана услуга")]
    public string CountryName { get; set; }

    [JsonInclude]
    public virtual int? DriverReportId { get; set; }

    [JsonInclude]
    [CustomDisplayAttribute("Номер", "")]
    public string RideNumber { get; set; }

    [JsonInclude]
    [CustomDisplayAttribute("Дата", "")]
    public DateTime? RideDate { get; set; }

    [JsonInclude]
    public virtual bool? IsDaily { get; set; }

    [JsonIgnore]
    [CustomDisplayAttribute("Ежедневный", "Укажите, если отчет ежедневный")]
    public bool IsDailyValue => IsDaily.HasValue ? IsDaily.Value : false;

    [JsonInclude]
    public virtual bool? IsMonthly { get; set; }

    [JsonIgnore]
    [CustomDisplayAttribute("Ежемесячный", "Укажите, если отчет ежемесячный")]
    public bool IsMonthlyValue => IsMonthly.HasValue ? IsMonthly.Value : false;

    [JsonInclude]
    [CustomDisplayAttribute("Карта определена вручную", "")]
    public bool? IsCardSelectedManually { get; set; }

    [JsonInclude]
    [CustomDisplayAttribute("Код региона", "Выберите код региона")]
    public int? RegionId { get; set; }

    [JsonInclude]
    [CustomDisplayAttribute("Регион", "Укажите регион")]
    public string RegionName { get; set; }

    [JsonInclude]
    [CustomDisplayAttribute("Номер чека", "Укажите номер чека")]
    public string CheckID { get; set; }

    [JsonInclude]
    [CustomDisplayAttribute("Сторно", "")]
    public bool? IsStorno { get; set; }

    [JsonInclude]
    [CustomDisplayAttribute("Отредактировано провайдером", "")]
    public bool? IsModified { get; set; }

    [JsonInclude]
    [CustomDisplayAttribute("Код у провайдера", "")]
    public string ProviderFuelType { get; set; }

    [JsonInclude]
    [CustomDisplayAttribute("Услуга у провайдера", "")]
    public string ProviderFuelTypeName { get; set; }

    public override string ToString()
    {
        return FuelHelper.GetFuelTransactionString(this);
    }
}
