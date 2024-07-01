namespace RSpeditionWEB.Validation
{
    public class Validate_DateTimeNullable : ValidationAttribute
    {
        public Validate_DateTimeNullable()
        {
        }

        public Validate_DateTimeNullable(string propertyName)
        {
            PropertyName = propertyName;
        }

        public string PropertyName { get; set; } = string.Empty;

        public override bool IsValid(object value)
        {
            this.ErrorMessage = string.Empty;
            DateTime dateBegin = new(year: 1753, month: 1, day: 1, hour:12, minute: 0, second: 0);
            DateTime dateEnd = new(year: 9999, month: 12, day: 31, hour: 11, minute: 59, second: 59);

            if (value != null)
            {
                var dateString = value.ToString();

                if (DateTime.TryParse(dateString, out DateTime date))
                {
                    if (date >= dateBegin && date <= dateEnd)
                    {
                        return true;
                    }
                    else
                    {
                        this.ErrorMessage = string.IsNullOrEmpty(PropertyName ?? string.Empty)
                            ? "Значение даты д.б. в пределах (1/1/1753 12:00:00 AM - 12/31/9999 11:59:59 PM) "
                            : $"Значение поля «{PropertyName ?? string.Empty}» д.б. в пределах (1/1/1753 12:00:00 AM - 12/31/9999 11:59:59 PM) ";
                        return false;
                    }
                }
                else
                {
                    this.ErrorMessage = string.IsNullOrEmpty(PropertyName ?? string.Empty) 
                        ? "Неверный формат даты " 
                        : $"Неверный формат даты в поле «{PropertyName ?? string.Empty}» ";
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
    }
}
