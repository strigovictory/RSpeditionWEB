namespace RSpeditionWEB.Models.ViewModels.Currencies
{
    [Display(Name = "Валюта")]
    public class CurrencyResponse : EntityModifiedResponse, IId 
    {
        public CurrencyResponse()
        {  }

        [CustomDisplayAttribute("Идентификатор валюты", "Задайте идентификатор валюты")]
        public int Id { get; set; }

        [CustomDisplayAttribute("Полное наименование валюты", "Укажите полное наименование валюты")]
        public string FullName { get; set; }

        [CustomDisplayAttribute("Наименование валюты", "Укажите наименование валюты")]
        public string Name { get; set; }

        [CustomDisplayAttribute("Код валюты", "Укажите код валюты")]
        public int Code { get; set; }

        [CustomDisplayAttribute("Полное наименование валюты (англ.)", "Укажите полное наименование валюты (англ.)")]
        public string FullNameEng { get; set; }
    }
}