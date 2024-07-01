#nullable disable

namespace RSpeditionWEB.Models.ViewModels.Currencies
{
    [Display(Name = "Курс валюты")]
    public class CurrencyRateView :  IId
    {
        public CurrencyRateView() { }

        [CustomDisplayAttribute("Идентификатор курса валюты", "Задайте идентификатор курса валюты")]
        public int Id { get; set; }

        [CustomDisplayAttribute("Конвертируемая валюта", "Укажите валюту, которая конвертируется")]
        [CustomForeignAttribute(typeof(CurrencyResponse), "Name")]
        public int CurrencyId { get; set; }

        [CustomDisplayAttribute("Количество единиц валюты, в которую конвертируется", "Укажите количество единиц валюты, в которую конвертируется")]
        public int? ToCurrencyUnits { get; set; }

        [CustomDisplayAttribute("Валюта, в которую конвертируется", "Укажите валюту, в которую конвертируется")]
        [CustomForeignAttribute(typeof(CurrencyResponse), "Name")]
        public int ToCurrencyId { get; set; }

        [CustomDisplayAttribute("Дата курса", "Укажите дату курса")]
        public DateTime DateRate { get; set; }

        [CustomDisplayAttribute("Курс валюты", "Укажите курс валюты")]
        public decimal Rate { get; set; }
    }
}
