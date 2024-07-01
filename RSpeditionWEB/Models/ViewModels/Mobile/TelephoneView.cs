namespace RSpeditionWEB.Models.ViewModels.Mobile
{
    [Display(Name = "Телефонный номер")]
    public class TelephoneView : IId
    {
        public TelephoneView() { }

        [CustomDisplayAttribute("Идентификационный номер", "Укажите идентификационный номер")]
        public int Id { get; set; }

        [CustomDisplayAttribute("Номер телефона", "Введите номер телефона")]
        public string Nomber { get; set; }
    }
}
