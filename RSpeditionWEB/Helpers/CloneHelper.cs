namespace RSpeditionWEB.Helpers
{
    public static class CloneHelper
    {
        public static FuelCardModel CloneCarFuelCard(this FuelCardModel cardToClone)
            => new FuelCardModel
            {
                Id = cardToClone.Id,
                Number = cardToClone.Number,
                FullNumber = cardToClone.FullNumber,
                ExpirationDate = cardToClone.ExpirationDate,
                ReceiveDate = cardToClone.ReceiveDate,
                IsReserved = cardToClone.IsReserved,
                IsArchived = cardToClone.IsArchived,
                CarId = cardToClone.CarId,
                DivisionID = cardToClone.DivisionID,
                EmployeeID = cardToClone.EmployeeID,
                ProviderId = cardToClone.ProviderId,
                Note = cardToClone.Note,
            };

        public static FuelTransactionFullResponse Clone_FuelCardsTranz(this FuelTransactionFullResponse tranzToClone)
            => new FuelTransactionFullResponse
                {
                    Id = tranzToClone.Id,
                    TransactionID = tranzToClone.TransactionID,
                    OperationDate = tranzToClone.OperationDate,
                    Quantity = tranzToClone.Quantity,
                    Cost = tranzToClone.Cost,
                    TotalCost = tranzToClone.TotalCost,
                    IsCheck = tranzToClone.IsCheck,
                    ProviderId = tranzToClone.ProviderId,
                    ProviderName = tranzToClone.ProviderName,
                    FuelTypeId = tranzToClone.FuelTypeId,
                    FuelTypeName = tranzToClone.FuelTypeName,
                    CurrencyId = tranzToClone.CurrencyId,
                    CurrencyName = tranzToClone.CurrencyName,
                    CardId = tranzToClone.CardId,
                    CardName = tranzToClone.CardName,
                    CountryId = tranzToClone.CountryId,
                    CountryName = tranzToClone.CountryName,
                    DivisionName = tranzToClone.DivisionName,
                    IsDaily = tranzToClone.IsDaily,
                    IsMonthly = tranzToClone.IsMonthly,
                    DriverReportId = tranzToClone.DriverReportId,
                    RideNumber = tranzToClone.RideNumber,
                    RideDate = tranzToClone.RideDate,
                    IsCardSelectedManually = tranzToClone.IsCardSelectedManually,
                    RegionId = tranzToClone.RegionId,
                    RegionName = tranzToClone.RegionName,
                    CheckID = tranzToClone.CheckID,
                    IsStorno = tranzToClone.IsStorno,
                    IsModified = tranzToClone.IsModified,
                    ProviderFuelType = tranzToClone.ProviderFuelType,
                    ProviderFuelTypeName = tranzToClone.ProviderFuelTypeName,
                    ModifiedBy = tranzToClone.ModifiedBy,
                    ModifiedOn = tranzToClone.ModifiedOn,
                };
    }
}
