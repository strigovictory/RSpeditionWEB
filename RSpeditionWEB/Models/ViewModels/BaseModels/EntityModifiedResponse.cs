namespace RSpeditionWEB.Models.ViewModels.BaseModels
{
    public abstract class EntityModifiedResponse
    {
        [JsonInclude]
        [CustomDisplayAttribute("Дата внесения изменений", "Укажите дату внесения изменений")]
        public DateTime? ModifiedOn { get; set; }

        [JsonInclude]
        [CustomDisplayAttribute("Имя пользователя, который внес изменение", "Укажите имя пользователя, который внес изменение")]
        public string ModifiedBy { get; set; }
    }
}
