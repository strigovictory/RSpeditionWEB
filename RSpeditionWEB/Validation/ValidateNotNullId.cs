namespace RSpeditionWEB.Validation
{
    internal class ValidateNotNullId : ValidationAttribute
    {
        public ValidateNotNullId() { }

        public ValidateNotNullId(string propName) 
        {
            PropName = propName;
        }

        public string PropName { get; set; } = string.Empty;

        public override bool IsValid(object value)
        {
            this.ErrorMessage = string.Empty;

            if (value != null)
            {
                var idString = value.ToString();

                if(!string.IsNullOrEmpty(idString) && !string.IsNullOrWhiteSpace(idString))
                {

                    if (Int32.TryParse(idString, out int idInt))
                    {
                        if (idInt > 0)
                        {
                            return true;
                        }
                        else
                        {
                            this.ErrorMessage = $"Заполните поле «{this.PropName ?? string.Empty}»! ";
                            return false;
                        }
                    }
                    else
                    {
                        this.ErrorMessage = $"Заполните поле «{this.PropName ?? string.Empty}»! ";
                        return false;
                    }
                }
                else
                {
                    this.ErrorMessage = $"Заполните поле «{this.PropName ?? string.Empty}»! ";
                    return false;
                }
            }
            else
            {
                this.ErrorMessage = $"Заполните поле «{this.PropName ?? string.Empty}»! ";
                return false;
            }
        }
    }
}
