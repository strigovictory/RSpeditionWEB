namespace RSpeditionWEB.Models.ViewModels.Banks
{
    public class BanksCardsGroupModel : ICloneable
    {
        public BanksCardsGroupModel() 
        {
            this.Cards = new();
        }

        [ValidateIEnumerable(CollectionName = "банковских карт")]
        public List<EmployeeCreditCard_View_GUID> Cards { get; private set; }

        [CustomDisplayAttribute("Банк", "Выберите банк")]
        public int? BankId { get; set; }

        public object Clone()
        => (object)
           new BanksCardsGroupModel
           {
               Cards = this.Cards?.Select(_ => _.Clone())?.Cast<EmployeeCreditCard_View_GUID>()?.ToList() ?? new(),
               BankId = this.BankId
           };
    }

    public class EmployeeCreditCard_View_GUID : EmployeeCreditCardView, ICloneable, IGuid
    {
        public string GuideId { get; set; }
        public string ManualValidationMessage_IssueDate { get; set; } = String.Empty;
        public string ManualValidationMessage_ExpirDate { get; set; } = String.Empty;

        public EmployeeCreditCard_View_GUID()
        {
            this.GuideId = Guid.NewGuid().ToString() ?? string.Empty;
        }

        public override object Clone()
            => (object)new EmployeeCreditCard_View_GUID
            {
                GuideId = this.GuideId,
                Id = this.Id,
                CardNumber = this.CardNumber,
                Rbsnumber = this.Rbsnumber,
                ExpirationMonth = this.ExpirationMonth,
                ExpirationYear = this.ExpirationYear,
                DateOfIssue = this.DateOfIssue,
                Comment = this.Comment,
                Hidden = this.Hidden,
                BankId = this.BankId,
                BankName = this.BankName,
                EmployeeId = this.EmployeeId,
                EmployeeName = this.EmployeeName,
                CurrencyId = this.CurrencyId,
                CurrencyName = this.CurrencyName,
                CardType = this.CardType
            };

        protected Func<DateTime?, DateTime?, bool> IsExpireDate_Later_IssueDate 
            = (expire, issue) 
            => expire.HasValue && issue.HasValue 
            ? expire >= issue
            : true;

        public void CamparisonIssueExpireDates()
        {
            if (this.ExpirationMonth.HasValue && this.ExpirationYear.HasValue)
            {
                var expireDate = new DateTime(day: DateTime.DaysInMonth(this.ExpirationYear ?? 0, this.ExpirationMonth ?? 0),
                                              month: this.ExpirationMonth.Value,
                                              year: this.ExpirationYear.Value);
                if (!this.IsExpireDate_Later_IssueDate(expireDate, this.DateOfIssue))
                    this.ManualValidationMessage_IssueDate = $"Дата выдачи «{this.DateOfIssue.Value.FormatingDate() ?? string.Empty}» " +
                                                             $"д.б. раньше даты окончания срока действия карты «{expireDate.FormatingDate() ?? string.Empty}» !";
                else
                    this.ManualValidationMessage_IssueDate = string.Empty;
            }
            else
                this.ManualValidationMessage_IssueDate = string.Empty;
        }

        public void IssueDate_WereChanged(ChangeEventArgs val)
        {
            if (!DateTime.TryParse(val?.Value?.ToString(), out var res))
            {
                this.DateOfIssue = null;
                this.ManualValidationMessage_IssueDate = string.Empty;
            }
            else
            {
                this.DateOfIssue = res;
                this.CamparisonIssueExpireDates();
            }
        }

        public Func<int?, int?, bool> IsExpireMonth_And_ExpireYear_BothFillOrNull = (month, year) =>
        {
            if (month != null && year != null) return true;
            else if (month == null && year == null) return true;
            else return false;
        };

        public void ExpireDate_WereChanged()
        {
            if (!this.IsExpireMonth_And_ExpireYear_BothFillOrNull(this.ExpirationMonth, this.ExpirationYear))
                this.ManualValidationMessage_ExpirDate = $"При указании срока действия карты, необходимо заполнить месяц и год !";
            else
            {
                this.ManualValidationMessage_ExpirDate = string.Empty;
            }
            this.CamparisonIssueExpireDates();
        }

        public void ResetManualValidMessages()
        {
            this.ManualValidationMessage_IssueDate = string.Empty;
            this.ManualValidationMessage_ExpirDate = string.Empty;
        }
    }
}
