namespace RSpeditionWEB.Validation
{
    public class ValidateIdAttribute : ValidationAttribute
    {
        public string PropName { get; set; }
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                var idString = value.ToString();

                if (!string.IsNullOrEmpty(idString) && !string.IsNullOrWhiteSpace(idString))
                {

                    if (int.TryParse(idString, out int idInt))
                    {
                        if (idInt > 0)
                        {
                            return true;
                        }
                        else
                        {
                            ErrorMessage = $"Заполните поле «{PropName ?? string.Empty}»";
                            return false;
                        }
                    }
                    else
                    {
                        ErrorMessage = $"Заполните поле «{PropName ?? string.Empty}»";
                        return false;
                    }
                }
                else
                {
                    ErrorMessage = $"Заполните поле «{PropName ?? string.Empty}»";
                    return false;
                }
            }
            else
            {
                ErrorMessage = $"Заполните поле «{PropName ?? string.Empty}»";
                return false;
            }
        }
    }
}
