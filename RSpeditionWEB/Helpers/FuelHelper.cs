using System.Diagnostics;

namespace RSpeditionWEB.Helpers
{
    public static class FuelHelper
    {
        public static string GetFuelTransactionString(this FuelTransactionFullResponse transaction)
            => $"№{(string.IsNullOrEmpty(transaction.TransactionID) ? "б/н" : transaction.TransactionID)} " +
            $"{(transaction.OperationDate != default ? $"от {transaction.OperationDate.ToString("dd.MM.yyyy HH:mm:ss")}" : string.Empty)} " +
            $"по карте: {transaction.CardName ?? string.Empty}, " +
            $"количество:{String.Format("{0:N2}", transaction.Quantity ?? 0M)}, " +
            $"цена: {String.Format("{0:N3}", transaction?.Cost ?? 0M)}, " +
            $"сумма: {String.Format("{0:N3}", transaction?.TotalCost ?? 0M)}, " +
            $"{(string.IsNullOrEmpty(transaction.ProviderFuelType) ? string.Empty : $"код типа топлива: {transaction.ProviderFuelType ?? string.Empty}")} " +
            $"{(string.IsNullOrEmpty(transaction.ProviderFuelTypeName) ? string.Empty : $"наименование типа топлива: {transaction.ProviderFuelTypeName ?? string.Empty}")} " +
            $"{transaction.CountryName.ToString() ?? string.Empty} " +
            $"{transaction.CurrencyName.ToString() ?? string.Empty} " +
            $"{transaction.RegionId?.ToString() ?? string.Empty} " +
            $"{transaction.RegionName ?? string.Empty} " +
            $"{transaction.CheckID ?? string.Empty} " +
            $"{(transaction.IsStorno.HasValue && transaction.IsStorno.Value ? "сторнирована провайдером " : string.Empty)} " +
            $"{(transaction.IsModified.HasValue && transaction.IsModified.Value ? "изменена провайдером " : string.Empty)} " +
            $"{(transaction.IsDaily.HasValue && transaction.IsDaily.Value ? "ежедневный отчет " : string.Empty)} " +
            $"{(transaction.IsMonthly.HasValue && transaction.IsMonthly.Value ? "ежемесячный отчет " : string.Empty)} " +
            $"{(transaction.IsCardSelectedManually.HasValue && transaction.IsCardSelectedManually.Value ? "карта выбрана вручную " : string.Empty)} ";

        public static string GetFuelTransactionString(this FuelTransactionResponse transaction)
            => $"№{(string.IsNullOrEmpty(transaction.TransactionID) ? "б/н" : transaction.TransactionID)} " +
            $"{(transaction.OperationDate != default ? $"от {transaction.OperationDate.ToString("dd.MM.yyyy HH:mm:ss")}" : string.Empty)} " +
            $"количество:{String.Format("{0:N2}", transaction.Quantity ?? 0M)}, " +
            $"цена: {String.Format("{0:N3}", transaction?.Cost ?? 0M)}, " +
            $"сумма: {String.Format("{0:N3}", transaction?.TotalCost ?? 0M)}, " +
            $"{(string.IsNullOrEmpty(transaction.ProviderFuelType) ? string.Empty : $"код типа топлива: {transaction.ProviderFuelType ?? string.Empty}")} " +
            $"{(string.IsNullOrEmpty(transaction.ProviderFuelTypeName) ? string.Empty : $"наименование типа топлива: {transaction.ProviderFuelTypeName ?? string.Empty}")} " +
            $"{transaction.RegionId?.ToString() ?? string.Empty} " +
            $"{transaction.RegionName ?? string.Empty} " +
            $"{transaction.CheckID ?? string.Empty} " +
            $"{(transaction.IsStorno.HasValue && transaction.IsStorno.Value ? "сторнирована провайдером " : string.Empty)} " +
            $"{(transaction.IsModified.HasValue && transaction.IsModified.Value ? "изменена провайдером " : string.Empty)} " +
            $"{(transaction.IsDaily.HasValue && transaction.IsDaily.Value ? "ежедневный отчет " : string.Empty)} " +
            $"{(transaction.IsMonthly.HasValue && transaction.IsMonthly.Value ? "ежемесячный отчет " : string.Empty)} " +
            $"{(transaction.IsCardSelectedManually.HasValue && transaction.IsCardSelectedManually.Value ? "карта выбрана вручную " : string.Empty)} ";

        public static string GetFuelTransactionString(this FuelTransactionShortResponse transaction)
            => $"№{(string.IsNullOrEmpty(transaction.TransactionID) ? "б/н" : transaction.TransactionID)} " +
            $"{(transaction.OperationDate != default ? $"от {transaction.OperationDate.ToString("dd.MM.yyyy HH:mm:ss")}" : string.Empty)} " +
            $"количество:{String.Format("{0:N2}", transaction.Quantity ?? 0M)}, " +
            $"цена: {String.Format("{0:N3}", transaction?.Cost ?? 0M)}, " +
            $"сумма: {String.Format("{0:N3}", transaction?.TotalCost ?? 0M)}, " +
            $"{(transaction.IsDaily.HasValue && transaction.IsDaily.Value ? "ежедневный отчет " : string.Empty)} " +
            $"{(transaction.IsMonthly.HasValue && transaction.IsMonthly.Value ? "ежемесячный отчет " : string.Empty)} ";

        public static List<FuelTransactionFullResponse> MapFuelTranzGroupModelToSendToDB(this FuelTranzGroupModel groupTranz)
        {
            List<FuelTransactionFullResponse> res = new();

            foreach (var tranz in groupTranz.Tranzactions.Values)
            {
                res.Add(new FuelTransactionFullResponse
                    {
                        TransactionID = string.Empty,
                        OperationDate = new DateTime(day: tranz.OperationDateDay.Value.Day,
                                                 month: tranz.OperationDateDay.Value.Month,
                                                 year: tranz.OperationDateDay.Value.Year,
                                                 hour: tranz.OperationDateTimeHour,
                                                 minute: tranz.OperationDateTimeMin,
                                                 second: tranz.OperationDateTimeSec),
                        Quantity = tranz.Quantity,
                        Cost = tranz.Cost,
                        TotalCost = tranz.TotalCost,
                        ProviderId = groupTranz.FuelProviderId,
                        FuelTypeId =  tranz.FuelTypeId,
                        CurrencyId = tranz.CurrencyId,
                        CardId = tranz.CardId,
                        CountryId = tranz.CountryId
                });
            }

            return res;
        }
    }
}
