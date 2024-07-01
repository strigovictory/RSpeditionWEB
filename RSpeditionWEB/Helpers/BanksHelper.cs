namespace RSpeditionWEB.Helpers
{
    public static class BanksHelper
    {
        public static string GetBanksOperationString(this BanksCardsOperation_iew operation,
                                                     IEnumerable<CurrencyResponse> currencies,
                                                     IEnumerable<EmployeeCreditCardView> banksCards,
                                                     IEnumerable<EmployeeView> empl)
        => $"Банковская операция от {operation?.OperationDate.Date.FormatingDate() ?? string.Empty}» " +
           $"на сумму {operation?.AmountOperationOriginal.FormatingDecimalToString() ?? string.Empty} " +
           $"{currencies?.FirstOrDefault(_ => _.Id == operation?.CurrencyOperationId)?.Name ?? string.Empty} " +
           $"по карте с номером RBS «{banksCards?.FirstOrDefault(_ => _.Id == operation.EmployeeCreditCardId)?.Rbsnumber ?? "б/н"}» " +
           $"(сотрудник {empl?.FirstOrDefault(_ => _.Id == operation.EmployeeId)?.ToString() ?? String.Empty})";
    }
}
