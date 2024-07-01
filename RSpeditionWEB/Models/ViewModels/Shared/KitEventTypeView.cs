namespace RSpeditionWEB.Models.ViewModels.Shared;

[CustomDisplayAttribute("Разновидность событий")]
public class KitEventTypeView: IId
{
    public KitEventTypeView()
    {
    }

    [JsonInclude]
    [CustomDisplayAttribute("Идентификатор разновидности события", "Задайте идентификатор разновидности события")]
    public int Id {get; set;}

    [JsonInclude]
    [Required(ErrorMessage = "Укажите наименование разновидности события")]
    [CustomDisplayAttribute("Наименование разновидности события", "Укажите наименование разновидности события")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Наименование разновидности события должно содержать не менее 3 и не более 50 символов")]
    public string Name { get; set; } = string.Empty;
}
