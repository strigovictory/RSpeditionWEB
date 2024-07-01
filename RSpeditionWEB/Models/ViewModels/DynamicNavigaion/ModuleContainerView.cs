namespace RSpeditionWEB.Models.ViewModels.DynamicNavigation;

// Контейнер для таблиц
public partial class ModuleContainerView : IId
{
    public ModuleContainerView()
    {
    }

    [JsonInclude]
    [JsonPropertyName(nameof(Id))]
    public int Id { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(Name))]
    [Required(ErrorMessage = "Выберите наименование настроек таблицы")]
    [CustomDisplayAttribute("Наименование настроек таблицы", "Выберите наименование настроек таблицы")]
    public string Name { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(SortOrder))]
    [Required(ErrorMessage = "Выберите порядок сортировки")]
    [CustomDisplayAttribute("Порядок сортировки", "Выберите порядок сортировки")]
    public int SortOrder { get; set; } // порядок следования категории на панели навигации внутри блока справочников

    [JsonInclude]
    [JsonPropertyName(nameof(ChangedDate))]
    public DateTime ChangedDate { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(ChangedUsername))]
    public string ChangedUsername { get; set; }
}
