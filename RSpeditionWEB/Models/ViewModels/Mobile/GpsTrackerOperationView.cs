namespace RSpeditionWEB.Models.ViewModels.Mobile;

public class GpsTrackerOperationView : ICloneable
{
    [CustomDisplayAttribute("Идентификационный номер", "Укажите идентификационный номер")]
    public int Id { get; set; }

    [Required]
    public virtual int OperationType { get; set; }

    [CustomDisplayAttribute("Тип события", "Выберите тип события")]
    public string OperationTypeName { get; set; }

    [CustomDisplayAttribute("Дата начала", "Укажите дату начала события")]
    [ValidateDateTime("Дата начала")]
    public DateTime OperationDate { get; set; }

    [CustomDisplayAttribute("Дата окончания", "Укажите дату окончания события")]
    [Validate_DateTimeNullable("Дата окончания")]
    public DateTime? OperationFinishDate { get; set; }

    [CustomDisplayAttribute("Комментарий", "Укажите комментарий")]
    [StringLength(250, ErrorMessage = "Комментарий должен содержать не более {1} символов.")]
    public string Comment { get; set; }

    [CustomDisplayAttribute("Мобильный номер", "Укажите мобильный номер")]
    [StringLength(25, ErrorMessage = "Номер должен содержать не более {1} символов.")]
    public string PhoneNumber { get; set; }

    [ValidateId(PropName = "GPS-трекер")]
    public virtual int FkGpstrackerId { get; set; }

    [CustomDisplayAttribute("GPS-трекер", "Выберите трекер")]
    public string GpstrackerName { get; set; }

    public virtual int? FkTruckId { get; set; }

    [CustomDisplayAttribute("Тягач", "Выберите тягач")]
    public string TruckName { get; set; }

    public virtual int? FkTrailerId { get; set; }

    [CustomDisplayAttribute("Прицеп", "Выберите прицеп")]
    public string TrailerName { get; set; }

    public virtual int? FkSimcardId { get; set; }

    [CustomDisplayAttribute("Сим-карта", "Выберите сим-карту")]
    public string SimcardName { get; set; }

    [CustomDisplayAttribute("Пользователь, добавивший событие", "Выберите пользователя, добавившего событие")]
    public string UserNameInsert { get; set; }

    [CustomDisplayAttribute("Дата добавления", "Укажите дату добавления события")]
    public DateTime? UserInsertDate { get; set; }

    [CustomDisplayAttribute("Пользователь, обновивший событие", "Выберите пользователя, обновившего событие")]
    public string UserNameUpdate { get; set; }

    [CustomDisplayAttribute("Дата обновления", "Укажите дату обновления события")]
    public DateTime? UserUpdateDate { get; set; }

    public object Clone()
    => new GpsTrackerOperationView
    {
        Id = this.Id,
        OperationType = this.OperationType,
        OperationTypeName = this.OperationTypeName,
        OperationDate = this.OperationDate,
        OperationFinishDate = this.OperationFinishDate,
        Comment = this.Comment,
        PhoneNumber = this.PhoneNumber,
        FkGpstrackerId = this.FkGpstrackerId,
        GpstrackerName = this.GpstrackerName,
        FkTruckId = this.FkTruckId,
        TruckName = this.TruckName,
        FkTrailerId = this.FkTrailerId,
        TrailerName = this.TrailerName,
        FkSimcardId = this.FkSimcardId,
        SimcardName = this.SimcardName,
        UserNameInsert = this.UserNameInsert,
        UserInsertDate = this.UserInsertDate,
        UserNameUpdate = this.UserNameUpdate,
        UserUpdateDate = this.UserUpdateDate
    };
}
