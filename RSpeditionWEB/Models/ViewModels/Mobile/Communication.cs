namespace RSpeditionWEB.Models.ViewModels.Mobile
{
    public enum CommunicationKind : int { CallsOutgoing = 1, CallsIncoming, SMS, GPRS };

    public class CommunicationSettings
    {
        [JsonInclude]
        [JsonPropertyName(nameof(Year))]
        [CustomDisplayAttribute("Год", "Укажите год")]
        public int Year { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(Month))]
        [CustomDisplayAttribute("Месяц", "Укажите месяц")]
        public int Month { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(DivisionId))]
        [CustomDisplayAttribute("Подразделение", "Укажите подразделение")]
        [Required(ErrorMessage = "Подразделение обязательно для указания")]
        public int DivisionId { get; set; }
    }
    public class Communication: CommunicationSettings
    {
        [JsonInclude]
        [JsonPropertyName(nameof(CommunicationTime))]
        [CustomDisplayAttribute("Дата", "Выберите дату")]
        public DateTime CommunicationTime { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(SourcePhone))]
        public TelephoneView SourcePhone { get; set; }

        [JsonIgnore]
        [CustomDisplayAttribute("Исходящий номер", "Укажите исходящий номер")]
        public string SourcePhoneName => this.SourcePhone?.Nomber ?? "«Номер не обнаружен»";

        [JsonInclude]
        [JsonPropertyName(nameof(DestinationPhone))]
        public TelephoneView DestinationPhone { get; set; }

        [JsonIgnore]
        [CustomDisplayAttribute("Входящий номер", "Укажите входящий номер")]
        public string DestinationPhoneName => this.DestinationPhone?.Nomber ?? "«Номер не обнаружен»";

        [JsonInclude]
        [JsonPropertyName(nameof(DestinationCountry))]
        [CustomDisplayAttribute("Страна адресата", "Укажите страну адресата")]
        public string DestinationCountry { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(Cost))]
        [CustomDisplayAttribute("Стоимость", "Укажите стоимость")]
        public decimal Cost { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(IsCompanyPay))]
        [CustomDisplayAttribute("За счет компании", "Укажите, если компенсация стоимости производится за счет компании")]
        public bool IsCompanyPay { get; set; }
    }

    [Display(Name = "Звонки")]
    public class Communication_call : Communication
    {
        [JsonInclude]
        [JsonPropertyName(nameof(CallKindString))]
        [CustomDisplayAttribute("Направление звонка", "Выберите направление звонка")]
        public string CallKindString { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(Length))]
        [CustomDisplayAttribute("Продолжительность звонка", "Укажите продолжительность звонка")]
        public string Length { get; set; }

        public override string ToString()
        =>  $"Телефонный звонок " +
            $"на сумму {this.Cost.FormatingDecimalToString5()} " +
            $"с номера {this.SourcePhoneName} " +
            $"на номер {this.DestinationPhoneName} " +
            $"от {this.CommunicationTime.ToString("dd.MM.yyyy HH:mm:ss")} " +
            $"продолжительностью {this.Length}";
    }

    [Display(Name = "SMS")]
    public class Communication_sms : Communication
    {
        [JsonInclude]
        [JsonPropertyName(nameof(SmsKindString))]
        [CustomDisplayAttribute("Тип", "Выберите тип смс")]
        public string SmsKindString { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(SourceCountry))]
        [CustomDisplayAttribute("Страна отправителя", "Укажите страну отправителя")]
        public string SourceCountry { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(Parts))]
        [CustomDisplayAttribute("Части", "Укажите количество частей")]
        public int Parts { get; set; }

        public override string ToString()
        =>  $"SMS-сообщение " +
            $"из {this.Parts} частей " +
            $"на сумму {this.Cost.FormatingDecimalToString5()} " +
            $"с номера {this.SourcePhoneName} " +
            $"на номер {this.DestinationPhoneName} " +
            $"от {this.CommunicationTime.ToString("dd.MM.yyyy HH:mm:ss")} ";
    }

    [Display(Name = "GPRS")]
    public class Communication_gprs : Communication
    {
        public override string ToString()
        =>  $"GPRS " +
            $"на сумму {this.Cost.FormatingDecimalToString5()} " +
            $"с номера {this.SourcePhoneName} " +
            $"от {this.CommunicationTime.ToString("dd.MM.yyyy HH:mm:ss")} ";
    }

    public class MobileReportsUploadsResult : ResponseBase
    {
        public MobileReportsUploadsResult()
        {
            this.Communication_calls = new();
            this.Communication_smses = new();
            this.Communication_gprses = new();
        }

        public MobileReportsUploadsResult(string mess) : base(mess)
        {
            this.Communication_calls = new();
            this.Communication_smses = new();
            this.Communication_gprses = new();
        }

        [JsonInclude]
        [JsonPropertyName(nameof(Communication_calls))]
        public ResponseGroupActionDetailed<Communication_call, Communication_call> Communication_calls { get; set; } = new();

        [JsonInclude]
        [JsonPropertyName(nameof(Communication_smses))]
        public ResponseGroupActionDetailed<Communication_sms, Communication_sms> Communication_smses { get; set; } = new();

        [JsonInclude]
        [JsonPropertyName(nameof(Communication_gprses))]
        public ResponseGroupActionDetailed<Communication_gprs, Communication_gprs> Communication_gprses { get; set; } = new();
    }

    public class MobileReportsUploadsSuccessResult
    {
        public MobileReportsUploadsSuccessResult(List<Communication_call> calls,
                                                 List<Communication_sms> smses,
                                                 List<Communication_gprs> gprses)
        {
            this.Communication_calls = calls;
            this.Communication_smses = smses;
            this.Communication_gprses = gprses;
        }

        [JsonInclude]
        [JsonPropertyName(nameof(Communication_calls))]
        public List<Communication_call> Communication_calls { get; set; } = new();

        [JsonInclude]
        [JsonPropertyName(nameof(Communication_smses))]
        public List<Communication_sms> Communication_smses { get; set; } = new();

        [JsonInclude]
        [JsonPropertyName(nameof(Communication_gprses))]
        public List<Communication_gprs> Communication_gprses { get; set; } = new();
    }
}
