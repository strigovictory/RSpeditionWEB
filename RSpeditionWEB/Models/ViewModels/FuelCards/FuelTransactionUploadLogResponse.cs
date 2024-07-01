namespace RSpeditionWEB.Models.ViewModels.FuelCards;

[Display(Name = "Результат загрузки транзакции по топливной карте")]
public class FuelTransactionUploadLogResponse : IId
{
    [CustomDisplayAttribute("Идентификатор в БД", "")]
    public int Id { get; set; }

    [CustomDisplayAttribute("Дата транзакции", "")]
    public DateTime? OperationDate { get; set; }

    [JsonIgnore]
    [CustomDisplayAttribute("Время транзакции", "")]
    public string OperationDateTime => $"{(OperationDate.HasValue ? OperationDate.Value.FormatingTime() : string.Empty)}";

    [CustomDisplayAttribute("Количество", "")]
    public decimal Quantity { get; set; }

    [CustomDisplayAttribute("Стоимость", "")]
    public decimal? Cost { get; set; }

    [CustomDisplayAttribute("Общая стоимость", "")]
    public decimal? TotalCost { get; set; }

    [CustomDisplayAttribute("Идентификатор транзакции у провайдера", "")]
    public string TransactionId { get; set; }

    [CustomDisplayAttribute("ежедневный", "Укажите, если отчет ежедневный")]
    public bool IsDaily { get; set; }

    [CustomDisplayAttribute("ежемесячный", "Укажите, если отчет ежемесячный")]
    public bool IsMonthly { get; set; }

    [CustomForeignAttribute(typeof(FuelCardFullModel), nameof(FuelCardFullModel.Number))]
    public virtual int? CardId { get; set; }

    [CustomDisplayAttribute("Топливная карта", "Выберите топливную карту")]
    public string CardName { get; set; }

    [CustomForeignAttribute(typeof(FuelTypeResponse), nameof(FuelTypeResponse.Name))]
    public virtual int? FuelTypeId { get; set; }

    [CustomDisplayAttribute("Разновидность услуги/товара", "")]
    public string FuelTypeName { get; set; }

    [CustomForeignAttribute(typeof(CurrencyResponse), nameof(CurrencyResponse.Name))]
    public virtual int? CurrencyId { get; set; }

    [CustomDisplayAttribute("Валюта", "")]
    public string CurrencyName { get; set; }

    [CustomForeignAttribute(typeof(CountryResponse), nameof(CountryResponse.Name))]
    public virtual int? CountryId { get; set; }

    [CustomDisplayAttribute("Страна", "")]
    public string CountryName { get; set; }

    [CustomForeignAttribute(typeof(FuelProviderResponse), nameof(FuelProviderResponse.Name))]
    public virtual int ProviderId { get; set; }

    [CustomDisplayAttribute("Провайдер", "")]
    public string ProviderName { get; set; }

    [CustomDisplayAttribute("Источник загрузки", "")]
    public string UploadBy { get; set; }

    [CustomDisplayAttribute("Дата загрузки", "")]
    public DateTime UploadOn { get; set; }

    [CustomDisplayAttribute("Код результата загрузки", "")]
    public char UploadResultCode { get; set; }

    [CustomDisplayAttribute("Результат загрузки", "")]
    public string UploadResultMessage { get; set; }

    [CustomDisplayAttribute("Карт определена вручную", "")]
    public bool? IsCardSelectedManually { get; set; }

    [CustomDisplayAttribute("Код разновидности услуги/товара у провайдера", "")]
    public string ProviderFuelType { get; set; }

    [CustomDisplayAttribute("Наименование разновидности услуги/товара у провайдера", "")]
    public string ProviderFuelTypeName { get; set; }

    [CustomDisplayAttribute("Код региона", "")]
    public int? RegionId { get; set; }

    [CustomDisplayAttribute("Регион", "")]
    public string RegionName { get; set; }

    public override string ToString()
    {
        return $"Транзакция загружена в БД {UploadOn.ToString("dd.MM.yyyy HH:mm:ss")} !" +
            $"пользователем {UploadBy ?? string.Empty}, !" +
            $"код результата: {UploadResultCode}, !" +
            $"результат загрузки: {UploadResultMessage ?? "«»"}, !" +
            $"подробности по загруженной транзакции:! " +
            $"№{(string.IsNullOrEmpty(TransactionId) ? "б/н" : TransactionId)} !" +
            $"от {OperationDate?.GetStringDateTime() ?? "«»"}, !" +
            $"количество: {Quantity}, !" +
            $"цена: {Cost ?? 0}, !" +
            $"сумма: {TotalCost ?? 0}, !" +
            $"код разновидности услуги/товара у провайдера: {ProviderFuelType ?? string.Empty}, !" +
            $"наименование разновидности услуги/товара у провайдера: {ProviderFuelTypeName ?? string.Empty}, !" +
            $"{RegionId.ToString() ?? string.Empty} !" +
            $"{RegionName ?? string.Empty} !" +
            $"{(IsDaily ? "ежедневный отчет" : string.Empty)} !" +
            $"{(IsMonthly ? "ежемесячный отчет" : string.Empty)} !" +
            $"{(IsCardSelectedManually.HasValue && IsCardSelectedManually.Value ? "карта выбрана вручную" : string.Empty)} ";
    }
}