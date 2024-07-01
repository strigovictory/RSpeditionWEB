using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace RSpeditionWEB.Models.ViewModels
{
    [Display(Name = "Топливная карта")]
    [AutoMap(typeof(FuelCardShortModel), ReverseMap = true)]
    public class FuelCardFullModel : IId
    {
        [JsonInclude]
        [CustomDisplayAttribute("Идентификатор", "Задайте идентификатор топливной карты")]
        public int Id { get; set; }

        [JsonInclude]
        [Required(ErrorMessage = "Укажите номер топливной карты")]
        [CustomDisplayAttribute("Номер", "Укажите номер топливной карты")]
        [StringLength(25, ErrorMessage = "Номер топливной карты не может содержать более 25 символов")]
        public string Number { get; set; }

        [JsonInclude]
        [CustomDisplayAttribute("Полный номер", "Укажите полный номер топливной карты")]
        [StringLength(25, ErrorMessage = "Полный номер топливной карты не может содержать более 25 символов")]
        public string FullNumber { get; set; }

        [JsonInclude]
        [CustomDisplayAttribute("Срок действия", "Укажите срок действия топливной карты")]
        [Validate_DateTimeNullable("Дата окончания срока действия")]
        public DateTime? ExpirationDate { get; set; }

        [JsonInclude]
        [CustomDisplayAttribute("Дата поступления", "Укажите дату поступления топливной карты от поставщика топлива")]
        [Validate_DateTimeNullable("Дата поступления")]
        public DateTime? ReceiveDate { get; set; }

        [JsonInclude]
        [CustomDisplayAttribute("Резерв", "Отметьте, если топливная карта в резерве")]
        public bool IsReserved { get; set; }

        [JsonInclude]
        [CustomDisplayAttribute("Примечание", "Укажите примечание")]
        public string Note { get; set; }

        [JsonInclude]
        [CustomDisplayAttribute("Архив", "Отметьте, если топливная карта в архиве")]
        public bool IsArchived { get; set; }

        [JsonInclude]
        [CustomDisplayAttribute("Автомобиль", "Выберите автомобиль, за которым закреплена карта")]
        public virtual int? CarId { get; set; }

        [JsonInclude]
        [CustomDisplayAttribute("Автомобиль", "Выберите автомобиль, за которым закреплена карта")]
        public string CarName { get; set; }

        [JsonInclude]
        [CustomDisplayAttribute("Водитель", "Выберите водителя, за которым закреплена карта")]
        public virtual int? EmployeeID { get; set; }

        [JsonInclude]
        [CustomDisplayAttribute("Водитель", "Выберите водителя, за которым закреплена карта")]
        public string EmployeeName { get; set; }

        [JsonInclude]
        [Required(ErrorMessage = "Выберите подразделение, за которым закреплена карта")]
        [CustomDisplayAttribute("Подразделение", "Выберите подразделение, за которым закреплена карта")]
        [ValidateNotNullId]
        public virtual int DivisionID { get; set; }

        [JsonInclude]
        [CustomDisplayAttribute("Подразделение", "Выберите подразделение, за которым закреплена карта")]
        public string DivisionName { get; set; }

        [JsonInclude]
        [Required(ErrorMessage = "Выберите провайдера топлива, который обслуживает карту")]
        [ValidateId(PropName = "Провайдер")]
        [CustomDisplayAttribute("Провайдер", "Выберите провайдера топлива, который обслуживает карту")]
        public virtual int ProviderId { get; set; }

        [JsonInclude]
        [CustomDisplayAttribute("Провайдер", "Выберите провайдера топлива, который обслуживает карту")]
        public string ProviderName { get; set; }
    }
}
