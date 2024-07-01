namespace RSpeditionWEB.Models.ViewModels.Mobile
{
    public class GpstrackerInvoiceGroupModel : ICloneable
    {
        public GpstrackerInvoiceGroupModel()
        {
            this.Invoice = new();
            this.Trackers = new();
        }
        public GpsInvoiceView Invoice { get; set; }
        public List<GpsTracker_View_GUID> Trackers { get; set; }

        public object Clone()
        => (object)new GpstrackerInvoiceGroupModel
        {
            Invoice = this.Invoice.Clone() as GpsInvoiceView,
            Trackers = new(this.Trackers?.Select(_ => _.Clone())?.Cast<GpsTracker_View_GUID>())
        };
    }

    public class GpsTracker_View_GUID : GpsTrackerView, ICloneable, IGuid
    {
        public string GuideId { get; set; }
        public GpsTracker_View_GUID()
        {
            this.GuideId = Guid.NewGuid().ToString() ?? string.Empty;
        }

        public override object Clone()
        => (object) new GpsTracker_View_GUID
        {
            GuideId = this.GuideId,
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
    }
}
