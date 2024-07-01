namespace RSpeditionWEB.Validation
{
    internal class ValidateGUID : ValidationAttribute
    {
        public string PropName { get; set; }
        public override bool IsValid(object value)
        {
            Guid defaultGuid = default;
            if (Guid.TryParse((value?.ToString() ?? String.Empty), out var valParsed) && valParsed == defaultGuid)
            {
                this.ErrorMessage = $"Заполните поле «{this.PropName ?? string.Empty}»";
                return false;
            }
            else
                return true;
        }
    }
}
