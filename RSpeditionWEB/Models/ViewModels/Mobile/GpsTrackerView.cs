using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace RSpeditionWEB.Models.ViewModels.Mobile
{
    [Display (Name ="Трекер")]
    public class GpsTrackerView : IId, ICloneable
    {
        public GpsTrackerView()
        {
            this.Number = String.Empty;
            this.Imei = String.Empty;
            this.Cost = 0;
            this.Location = 1; // склад
        }

        [CustomDisplayAttribute("Идентификационный номер", "Укажите идентификационный номер")]
        [JsonInclude]
        [JsonPropertyName(nameof(Id))]
        public int Id { get; set; }

        [CustomDisplayAttribute("Номер трекера", "Укажите номер трекера")]
        [StringLength(15, ErrorMessage = "Номер должен содержать от {2} до {1} символов !", MinimumLength = 1)]
        [JsonInclude]
        [JsonPropertyName(nameof(Number))]
        public string Number { get; set; }

        [CustomDisplayAttribute("IMEI", "Укажите IMEI")]
        [StringLength(15, ErrorMessage = "IMEI должен содержать от {2} до {1} символов !", MinimumLength = 1)]
        [JsonInclude]
        [JsonPropertyName(nameof(Imei))]
        public string Imei { get; set; }

        [CustomDisplayAttribute("Стоимость", "Укажите стоимость")]
        [JsonInclude]
        [JsonPropertyName(nameof(Cost))]
        public decimal? Cost { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(Location))]
        public virtual int? Location { get; set; }

        [CustomDisplayAttribute("Местонахождение", "Укажите местонахождение")]
        [JsonIgnore]
        public string LocationName => this.Location.HasValue 
                                      && (OperationTypeHelper.OperationTypes_GpsTracker?.TryGetValue(this.Location.Value, out var res) ?? false) 
                                      ? 
                                      res.ToString() 
                                      :
                                      string.Empty
                                      ;

        [JsonInclude]
        [JsonPropertyName(nameof(FkTrackerModelId))]
        public virtual int? FkTrackerModelId { get; set; }

        [CustomDisplayAttribute("Модель", "Выберите модель трекера")]
        [JsonInclude]
        [JsonPropertyName(nameof(ModelName))]
        public string ModelName { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(FkInvoiceGuid))]
        public virtual Guid FkInvoiceGuid { get; set; }

        [CustomDisplayAttribute("Накладная", "Выберите накладную")]
        [JsonInclude]
        [JsonPropertyName(nameof(InvoiceName))]
        public string InvoiceName { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(FkDivisionId))]
        public virtual int? FkDivisionId { get; set; }

        [CustomDisplayAttribute("Подразделение", "Выберите подразделение")]
        [JsonInclude]
        [JsonPropertyName(nameof(DivisionName))]
        public string DivisionName { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(FkTruckId))]
        public virtual int? FkTruckId { get; set; }

        [CustomDisplayAttribute("Тягач", "Выберите тягач")]
        [JsonInclude]
        [JsonPropertyName(nameof(TruckName))]
        public string TruckName { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(FkTrailerId))]
        public virtual int? FkTrailerId { get; set; }

        [CustomDisplayAttribute("Прицеп", "Выберите прицеп")]
        [JsonInclude]
        [JsonPropertyName(nameof(TrailerName))]
        public string TrailerName { get; set; }

        public virtual object Clone()
        => (object)new GpsTrackerView
        {
            Id = this.Id,
            Number = this.Number,
            Imei = this.Imei,
            Cost = this.Cost,
            Location = this.Location,
            FkTrackerModelId = this.FkTrackerModelId,
            ModelName = this.ModelName,
            FkInvoiceGuid = this.FkInvoiceGuid,
            InvoiceName = this.InvoiceName,
            FkDivisionId = this.FkDivisionId,
            DivisionName = this.DivisionName,
            FkTruckId = this.FkTruckId,
            TruckName = this.TruckName,
            FkTrailerId = this.FkTrailerId,
            TrailerName = this.TrailerName
        };

        public override string ToString()
        => $"№ {this.Number ?? "б/н"}, IMEI: {this.Imei ?? "б/н"}";
    }
}
