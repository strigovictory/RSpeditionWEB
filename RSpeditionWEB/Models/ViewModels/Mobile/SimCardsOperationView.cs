namespace RSpeditionWEB.Models.ViewModels.Mobile
{
    public class SimCardsOperationView : ICloneable
    {
        public SimCardsOperationView() { }

        [JsonInclude]
        [JsonPropertyName(nameof(Id))]
        [CustomDisplayAttribute("Идентификационный номер", "Укажите идентификационный номер")]
        public int Id { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(OperationType))]
        [ValidateId(PropName = "Тип события")]
        public virtual int OperationType { get; set; }

        [CustomDisplayAttribute("Тип события", "Выберите тип события")]
        public string OperationTypeName { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(OperationDate))]
        [CustomDisplayAttribute("Дата начала", "Выберите дату начала события")]
        public DateTime OperationDate { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(OperationFinishDate))]
        [CustomDisplayAttribute("Дата завершения", "Выберите дату завершения события")]
        public DateTime? OperationFinishDate { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(Comment))]
        [CustomDisplayAttribute("Комментарий", "Укажите комментарий")]
        [StringLength(240, ErrorMessage = "Комментарий должен содержать не более {1} символов.")]
        public string Comment { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(UserNameInsert))]
        [CustomDisplayAttribute("Пользователь, добавивший событие", "Выберите пользователя, добавившего событие")]
        public string UserNameInsert { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(UserInsertDate))]
        [CustomDisplayAttribute("Дата добавления", "Укажите дату добавления события")]
        public DateTime? UserInsertDate { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(UserNameUpdate))]
        [CustomDisplayAttribute("Пользователь, обновивший событие", "Выберите пользователя, обновившего событие")]
        public string UserNameUpdate { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(UserUpdateDate))]
        [CustomDisplayAttribute("Дата обновления", "Укажите дату обновления события")]
        public DateTime? UserUpdateDate { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(SimCardId))]
        [ValidateId(PropName = "Sim-карта")]
        public virtual int SimCardId { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(SimCardName))]
        [CustomDisplayAttribute("Sim-карта", "Выберите sim-карту")]
        public string SimCardName { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(EmployeeId))]
        public virtual int? EmployeeId { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(EmployeeName))]
        [CustomDisplayAttribute("Работник", "Выберите работника")]
        public string EmployeeName { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(TrackerId))]
        public virtual int? TrackerId { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(TrackerName))]
        [CustomDisplayAttribute("Трекер", "Выберите трекер")]
        public string TrackerName { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(TruckId))]
        public virtual int? TruckId { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(TruckName))]
        [CustomDisplayAttribute("Тягач", "Выберите тягач")]
        public string TruckName { get; set; }

        public object Clone()
        => new SimCardsOperationView
        {
            Id = Id,
            OperationType = OperationType,
            OperationTypeName= OperationTypeName,
            OperationDate = OperationDate,
            OperationFinishDate = OperationFinishDate,
            Comment = Comment,
            UserNameInsert = UserNameInsert,
            UserInsertDate = UserInsertDate,
            UserNameUpdate = UserNameUpdate,
            UserUpdateDate = UserUpdateDate,
            SimCardId = SimCardId,
            SimCardName = SimCardName,
            EmployeeId = EmployeeId,
            EmployeeName = EmployeeName,
            TrackerId = TrackerId,
            TrackerName = TrackerName,
            TruckId = TruckId,
            TruckName = TruckName
        };
    }
}
