namespace RSpeditionWEB.Models.ViewModels.Banks;

/// <summary>
/// Справочник банковских счетов
/// </summary>
public class DivisionsBankAccount : EntityModifiedResponse, IId
{
    [Key]
    [JsonInclude]
    [JsonPropertyName(nameof(Id))]
    [CustomDisplayAttribute("Идентификатор банковского счета", "Задайте идентификатор банковкого счета")]
    [Column("bank_account_id")]
    public int Id { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(BankId))]
    [CustomDisplayAttribute("Идентификатор банка", "Задайте идентификатор банка")]
    [CustomForeignAttribute(typeof(BankView), "BankNameShort")]
    [Column("bank_id")]
    public int BankId { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(DivisionId))]
    [CustomDisplayAttribute("Идентификатор подразделения", "Задайте идентификатор подразделения")]
    [CustomForeignAttribute(typeof(DivisionResponse), "Name")]
    [Column("division_id")]
    public int DivisionId { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(CurrencyId))]
    [CustomDisplayAttribute("Идентификатор валюты", "Задайте идентификатор валюты")]
    [CustomForeignAttribute(typeof(CurrencyResponse), "Name")]
    [Column("currency_id")]
    public int CurrencyId { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(DateBegin))]
    [CustomDisplayAttribute("Дествует c", "Укажите с какой даты действет")]
    [Column("date_begin")]
    public DateTime DateBegin { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(DateEnd))]
    [CustomDisplayAttribute("Дествует по", "Укажите по какую дату действет")]
    [Column("date_end")]
    public DateTime? DateEnd { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(AccountNumber))]
    [CustomDisplayAttribute("Номер банковского счета", "Задайте банковского счета")]
    [Column("account_number")]
    public string AccountNumber { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(AccountNumberCorr))]
    [CustomDisplayAttribute("Номер корреспондирующего счета", "Укажите номер корреспондирующего счета")]
    [Column("account_number_corr")]
    public string AccountNumberCorr { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(Inn))]
    [CustomDisplayAttribute("Индивидуальный налоговый номер ИНН", "Укажите индивидуальный налоговый номер ИНН")]
    [Column("inn")]
    public string Inn { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(Kpp))]
    [CustomDisplayAttribute("Код причины постановки на учет КПП", "Укажите код причины постановки на учет КПП")]
    [Column("kpp")]
    public string Kpp { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(Bik))]
    [CustomDisplayAttribute("Банковский идентификационный код БИК", "Укажите банковский идентификационный код БИК")]
    public string Bik { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(Swift))]
    [CustomDisplayAttribute("SWIFT для международных переводов", "Укажите SWIFT для международных переводов")]
    public string Swift { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(Intermediary))]
    [CustomDisplayAttribute("Реквизиты банка", "Укажите реквизиты банка")]
    public string Intermediary { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(IntermediaryAddress))]
    [CustomDisplayAttribute("Адрес (банка корреспондента)", "Укажите адрес (банка корреспондента)")]
    [Column("intermediary_address")]
    public string IntermediaryAddress { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(IntermediaryInfo))]
    [CustomDisplayAttribute("Доп.информация (банка корреспондента)", "Укажите доп.информацию (банка корреспондента)")]
    [Column("intermediary_info")]
    public string IntermediaryInfo { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(IntermediaryAccountNumber))]
    [CustomDisplayAttribute("Номер счета (банка корреспондента)", "Укажите номер счета (банка корреспондента)")]
    [Column("intermediary_account_number")]
    public string IntermediaryAccountNumber { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(Beneficiary))]
    [CustomDisplayAttribute("Бенефициар", "Укажите бенефициара")]
    public string Beneficiary { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(IsDefault))]
    [CustomDisplayAttribute("Указатель на счет по умолчанию", "Укажите, является ли счет по умолчанию основным")]
    [Column("is_default")]
    public bool IsDefault { get; set; }

    [JsonIgnore]
    public virtual BankView Bank { get; set; }
}
