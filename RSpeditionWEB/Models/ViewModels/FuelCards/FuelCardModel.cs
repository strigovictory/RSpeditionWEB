using RSpeditionWEB.Validation;
using System.IO.Compression;

namespace RSpeditionWEB.Models.ViewModels.FuelCards;

[AutoMap(typeof(FuelCardFullModel), ReverseMap = true)]
[AutoMap(typeof(FuelCardShortModel), ReverseMap = true)]
public class FuelCardModel : ICloneable
{
    [JsonInclude]
    [CustomDisplayAttribute("Идентификатор в БД", "Задайте идентификатор топливной карты")]
    public int Id { get; set; }

    [JsonInclude]
    [Required(ErrorMessage = "Укажите номер топливной карты")]
    [CustomDisplayAttribute("Номер", "Номер топливной карты")]
    [StringLength(25, ErrorMessage = "Номер топливной карты должен содержать не менее 3 и не более 25 символов", MinimumLength = 3)]
    public string Number { get; set; } = string.Empty;

    [JsonInclude]
    [CustomDisplayAttribute("Полный номер", "Полный номер топливной карты")]
    [StringLength(25, ErrorMessage = "Полный номер топливной карты не может содержать более 25 символов")]
    public string FullNumber { get; set; }

    [JsonInclude]
    [CustomDisplayAttribute("Срок действия", "Срок действия")]
    [Validate_DateTimeNullable("Срок действия")]
    public DateTime? ExpirationDate { get; set; }

    [JsonInclude]
    [CustomDisplayAttribute("Дата поступления", "Дата поступления карты от поставщика")]
    [ValidateDateTime("Дата поступления")] // хоть поле и налэбл по БД, но дата поступления обязательна для заполнения
    public DateTime? ReceiveDate { get; set; }

    [JsonInclude]
    [CustomDisplayAttribute("Резерв", "Резерв")]
    public bool IsReserved { get; set; }

    [JsonInclude]
    [CustomDisplayAttribute("Примечание", "Примечание")]
    public string Note { get; set; } = string.Empty;

    [JsonInclude]
    [CustomDisplayAttribute("В архиве", "Отметьте, если топливная карта в архиве")]
    public bool IsArchived { get; set; }

    [JsonInclude]
    [CustomDisplayAttribute("Автомобиль", "Выберите автомобиль, за которым закреплена карта")]
    public int? CarId { get; set; }

    [JsonInclude]
    [CustomDisplayAttribute("Водитель", "Выберите водителя, за которым закреплена карта")]
    public int? EmployeeID { get; set; }

    [JsonInclude]
    [CustomDisplayAttribute("Подразделение", "Выберите подразделение, за которым закреплена карта")]
    [ValidateNotNullId("Подразделение")]
    public int DivisionID { get; set; }

    [JsonInclude]
    [ValidateNotNullId("Провайдер")]
    [CustomDisplayAttribute("Провайдер", "Провайдер")]
    public int ProviderId { get; set; }

    public object Clone()
     => new FuelCardModel
     {
         Id = Id,
         Number = Number,
         FullNumber = FullNumber,
         ExpirationDate = ExpirationDate,
         ReceiveDate = ReceiveDate,
         IsReserved = IsReserved,
         IsArchived = IsArchived,
         CarId = CarId,
         EmployeeID = EmployeeID,
         DivisionID = DivisionID,
         ProviderId = ProviderId,
         Note = Note,
     };

    public string ToStringShort() => Number ?? string.Empty;

    public override string ToString()
    {
        return string.IsNullOrEmpty(FullNumber)
        ? $"Заправочная карта «{Number ?? string.Empty}»"
        : $"Заправочная карта «{FullNumber ?? string.Empty} / {Number ?? string.Empty}»";
    }
}