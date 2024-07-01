namespace RSpeditionWEB.Models.ViewModels.Mobile
{
    public class SimCard_View_GroupModel_Parent : ICloneable
    {
        [CustomDisplayAttribute("Дата поступления", "Выберите дату поступления")]
        [ValidateDateTimeAttribute]
        public DateTime ReceiveDate { get; set; }

        [CustomDisplayAttribute("Дата списания", "Выберите дату списания")]
        [Validate_DateTimeNullable("Дата списания")]
        public DateTime? DiscardDate { get; set; }

        [CustomDisplayAttribute("Списана", "Отметьте, если sim-карта списана")]
        public bool Fdiscarded { get; set; }

        [CustomDisplayAttribute("Оператор", "Выберите оператора")]
        [Required(ErrorMessage = "Оператор должен быть выбран")]
        public string CellularOperator { get; set; }

        [ValidateIdAttribute(PropName = "Подразделение")]
        public int FkDivisionId { get; set; }

        [CustomDisplayAttribute("Подразделение", "Выберите подразделение")]
        public string DivisionName { get; set; }

        public List<SimCard_View_GroupModel_Child> SimCard_View_GroupModel_Childs { get; set; }

        public object Clone()
        {
            throw new NotImplementedException(); // Реализация не нужна
        }
    }

    public class SimCard_View_GroupModel_Child : IGuid
    {
        public string GuideId { get; set; }

        [CustomDisplayAttribute("Номер sim-карты", "Укажите номер sim-карты")]
        [StringLength(100, ErrorMessage = "Номер sim-карты должен содержать не более {1} символов.")]
        public string SimCardNumber { get; set; }

        [ValidateIdAttribute(PropName = "Телефонный номер")]
        public int FkTelephoneId { get; set; }

        [CustomDisplayAttribute("Телефонный номер", "Выберите телефонный номер")]
        public string TelephoneNumber { get; set; }
        public SimCard_View_GroupModel_Child()
        {
            this.GuideId = Guid.NewGuid().ToString() ?? string.Empty;
        }
    }
}
