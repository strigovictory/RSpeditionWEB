namespace RSpeditionWEB.Models.ViewModels.DynamicNavigation;

public partial class TableSettingView : IId
{
    [JsonInclude]
    [JsonPropertyName(nameof(Id))]
    public int Id { get; set; }

    [Required(ErrorMessage = "Выберите модуль-контейнер")]
    [CustomDisplayAttribute("Mодуль-контейнер", "Выберите модуль-контейнер")]
    [Range(1, Int32.MaxValue, ErrorMessage = "Выберите модуль-контейнер")]
    public int ModuleContainerId { get; set; } // ссылка на модуль, к которому относится текущая таблица

    [Required(ErrorMessage = "Укажите наименование для отображения")]
    [StringLength(50, ErrorMessage = "Наименование должно содержать от 3 до 50 символов.", MinimumLength = 3)]
    [CustomDisplayAttribute("Наименование для отображения", "Укажите наименование для отображения")]
    public string DisplayName { get; set; } // наименование для UI - значение для placeholder берется из значения атрибута Displey(Name), примененного к таблице и м.б. изменено пользователем на иное

    [Range(1, Int32.MaxValue, ErrorMessage = "Значение порядка сортировки должно быть больше единицы")]
    [CustomDisplayAttribute("Порядок сортировки", "Выберите порядок сортировки")]
    public int SortOrder { get; set; } // порядок следования внутри модуля 

    [Required(ErrorMessage = "Выберите таблицу из БД")]
    [StringLength(50, ErrorMessage = "Выберите таблицу из БД", MinimumLength = 1)]
    [CustomDisplayAttribute("Таблица из БД", "Выберите таблицу из БД")]
    public string DBName { get; set; } // наименование таблицы в БД с которой связана текущая модель

    [Required(ErrorMessage = "Выберите класс")]
    [StringLength(50, ErrorMessage = "Выберите класс", MinimumLength = 1)]
    [CustomDisplayAttribute("Класс", "Выберите класс")]
    public string ClassName { get; set; }

    [Required(ErrorMessage = "Выберите схему таблицы в БД")]
    [StringLength(50, ErrorMessage = "Выберите схему таблицы в БД", MinimumLength = 1)]
    [CustomDisplayAttribute("Схема таблицы в БД", "Выберите схему таблицы в БД")]
    public string DBSchemaName { get; set; } // наименование схемы таблицы в БД с которой связана текущая модель

    [CustomDisplayAttribute("Настройки столбцов таблицы", "Укажите настройки столбцов таблицы")]
    public string ColumnsSettings { get; set; }

    public string ChangedUsername { get; set; } // имя пользователя, добавившего запись 

    public DateTime ChangedDate { get; set; } // дата создания записи
}
