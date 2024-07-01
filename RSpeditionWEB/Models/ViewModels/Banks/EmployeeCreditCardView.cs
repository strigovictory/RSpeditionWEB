namespace RSpeditionWEB.Models.ViewModels;

[Display(Name = "Банковские карты")]
public partial class EmployeeCreditCardView : IId, ICloneable
{
    public EmployeeCreditCardView()
    {  }

    [CustomDisplayAttribute("Идентификационный номер", "Укажите идентификационный номер")]
    [JsonInclude]
    [JsonPropertyName(nameof(Id))]
    public int Id { get; set; }

    [CustomDisplayAttribute("Номер карты", "Укажите номер карты")]
    [StringLength(20, ErrorMessage = "Номер должен содержать от {2} до {1} символов! ", MinimumLength = 13)]
    [Required(ErrorMessage = "Номер карты должен быть указан! ")]
    [JsonInclude]
    [JsonPropertyName(nameof(CardNumber))]
    public string CardNumber { get; set; }

    [CustomDisplayAttribute("Номер RBS", "Укажите номер RBS")]
    [StringLength(31, ErrorMessage = "RBS-номер должен содержать от {2} до {1} символа! ", MinimumLength = 13)]
    [Required(ErrorMessage = "RBS-номер карты должен быть указан! ")]
    [JsonInclude]
    [JsonPropertyName(nameof(Rbsnumber))]
    public string Rbsnumber { get; set; }

    [CustomDisplayAttribute("Срок действия, месяц", "Укажите срок действия, месяц")]
    [JsonInclude]
    [JsonPropertyName(nameof(ExpirationMonth))]
    public virtual int? ExpirationMonth { get; set; }

    [CustomDisplayAttribute("Срок действия, месяц", "Укажите срок действия, месяц")]
    [JsonInclude]
    [JsonPropertyName(nameof(ExpirationMonthName))]
    public string ExpirationMonthName => this.ExpirationMonth?.GetStringMonth() ?? string.Empty;

    [CustomDisplayAttribute("Срок действия, год", "Укажите срок действия, год")]
    [JsonInclude]
    [JsonPropertyName(nameof(ExpirationYear))]
    public int? ExpirationYear { get; set; }

    [Validate_DateTimeNullable("Дата выдачи")]
    [CustomDisplayAttribute("Дата выдачи", "Укажите дату выдачи")]
    [JsonInclude]
    [JsonPropertyName(nameof(DateOfIssue))]
    public virtual DateTime? DateOfIssue { get; set; }

    [CustomDisplayAttribute("Дата выдачи", "Укажите дату выдачи")]
    [JsonInclude]
    [JsonPropertyName(nameof(DateOfIssueName))]
    public string DateOfIssueName => this.DateOfIssue?.FormatingDate() ?? string.Empty;

    [CustomDisplayAttribute("Комментарий", "Укажите комментарий")]
    [JsonInclude]
    [JsonPropertyName(nameof(Comment))]
    [StringLength(120, ErrorMessage = "Комментарий должен содержать не более {1} символа! ")]
    public string Comment { get; set; }

    [CustomDisplayAttribute("Скрыта", "Укажите, если карта скрыта")]
    [JsonInclude]
    [JsonPropertyName(nameof(Hidden))]
    public bool Hidden { get; set; }

    [CustomForeignAttribute(typeof(BankView), "BankName")]
    [JsonIgnore]
    public virtual int? BankId { get; set; }

    [CustomDisplayAttribute("Банк", "Выберите банк")]
    [JsonInclude]
    [JsonPropertyName(nameof(BankName))]
    public string BankName { get; set; }

    [CustomForeignAttribute(typeof(EmployeeView), "FullName")]
    [JsonInclude]
    [JsonPropertyName(nameof(EmployeeId))]
    [ValidateNotNullId(PropName = "«Сотрудник»")]
    public virtual int EmployeeId { get; set; }

    [CustomDisplayAttribute("Сотрудник", "Выберите сотрудника")]
    [JsonInclude]
    [JsonPropertyName(nameof(EmployeeName))]
    public string EmployeeName { get; set; }

    [CustomForeignAttribute(typeof(CurrencyResponse), "Name")]
    [JsonInclude]
    [JsonPropertyName(nameof(CurrencyId))]
    public virtual int? CurrencyId { get; set; }

    [CustomDisplayAttribute("Валюта", "Выберите валюту")]
    [JsonInclude]
    [JsonPropertyName(nameof(CurrencyName))]
    public string CurrencyName { get; set; }

    [CustomDisplayAttribute("Тип карты", "Выберите тип карты")]
    [Required(ErrorMessage = "Тип карты должен быть выбран! ")]
    [StringLength(20, ErrorMessage = "Тип карты должен содержать не менее {2} и не более {1} символов! ", MinimumLength = 1)]
    [JsonInclude]
    [JsonPropertyName(nameof(CardType))]
    public virtual string CardType { get; set; }

    [CustomDisplayAttribute("Тип карты", "Выберите тип карты")]
    [JsonIgnore]
    public string CardTypeName => this.CardTypes.TryGetValue(this.CardType ?? String.Empty, out var val) ? val : string.Empty;

    [JsonIgnore]
    public virtual Dictionary<string, string> CardTypes =>
        new Dictionary<string, string>
        {
            { "U", "UnionPay" },
            { "R", "Мир" },
            { "M", "MasterCard" },
            { "V", "Visa" }
        };

    public virtual object Clone()
    => new EmployeeCreditCardView
    {
        Id = this?.Id ?? 0,
        CardNumber = this?.CardNumber ?? string.Empty,
        Rbsnumber = this?.Rbsnumber ?? String.Empty,
        ExpirationMonth = this?.ExpirationMonth ?? null,
        ExpirationYear = this?.ExpirationYear ?? null,
        DateOfIssue = this?.DateOfIssue ?? null,
        Comment = this?.Comment ?? string.Empty,
        Hidden = this?.Hidden ?? false,
        BankId = this?.BankId ?? null,
        EmployeeId = this?.EmployeeId ?? 0,
        CurrencyId = this?.CurrencyId ?? null,
        CardType = this?.CardType ?? String.Empty
    };
}
