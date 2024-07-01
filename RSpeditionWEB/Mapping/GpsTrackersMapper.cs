namespace RSpeditionWEB.Mapping
{
    public static class GpsTrackersMapper
    {
        public static GpsTracker_View_GUID MapTo_GpsTrackerViewGUID(this GpsTrackerView item)
            => new GpsTracker_View_GUID
            {
                GuideId = Guid.NewGuid().ToString() ?? string.Empty,
                Id = item.Id,
                Number = item.Number,
                Imei = item.Imei,
                Cost = item.Cost,
                Location = item.Location,
                FkTrackerModelId = item.FkTrackerModelId,
                ModelName = item.ModelName,
                FkInvoiceGuid = item.FkInvoiceGuid,
                InvoiceName = item.InvoiceName,
                FkDivisionId = item.FkDivisionId,
                DivisionName = item.DivisionName,
                FkTruckId = item.FkTruckId,
                TruckName = item.TruckName,
                FkTrailerId = item.FkTrailerId,
                TrailerName = item.TrailerName
            };
    }
}
