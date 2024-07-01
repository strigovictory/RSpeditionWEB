namespace RSpeditionWEB.Models.ViewModels.Person
{
    [Display(Name = "Работник")]
    public class EmployeeView : EntityModifiedResponse, IId
    {
        [JsonInclude]
        [CustomDisplayAttribute("Идентификационный номер", "Укажите идентификационный номер")]
        public int Id { get; set; }

        [JsonInclude]
        public string LastName { get; set; }

        [JsonInclude]
        public string FirstName { get; set; }

        [JsonInclude]
        public string MiddleName { get; set; }

        [JsonInclude]
        public string LastNamePrevious { get; set; }

        [JsonInclude]
        [CustomDisplayAttribute("Фамилия, лат.", "Укажите фамилию латинскими буквами")]
        public string LastNamePassport { get; set; }

        [JsonInclude]
        [CustomDisplayAttribute("Имя, лат.", "Укажите имя латинскими буквами")]
        public string FirstNamePassport { get; set; }

        [JsonIgnore]
        [CustomDisplayAttribute("Полное имя", "Укажите полное имя")]
        public string FullName => $"{LastName ?? string.Empty} {FirstName?.Substring(0, 1) ?? string.Empty}.{MiddleName?.Substring(0, 1) ?? string.Empty}.";

        [JsonIgnore]
        [CustomDisplayAttribute("Полное имя", "Укажите полное имя")]
        public string FullLongName => $"{LastName ?? string.Empty} {FirstName ?? string.Empty} {MiddleName ?? string.Empty}";

        [JsonInclude]
        [CustomDisplayAttribute("Пол", "Укажите пол")]
        public string Gender { get; set; }

        [JsonInclude]
        [CustomDisplayAttribute("Не активен", "Укажите, если работник не активен")]
        public bool? IsActive { get; set; }

        public override string ToString()
        => !string.IsNullOrEmpty(this.FullLongName) ? this.FullLongName :
           !string.IsNullOrEmpty(this.FullName) ? this.FullName :
           !string.IsNullOrEmpty(this.LastNamePassport) || !string.IsNullOrEmpty(this.FirstNamePassport) ? ((this.LastNamePassport ?? String.Empty) + " " + (this.FirstNamePassport ?? String.Empty)):
            string.Empty;
    }
}
