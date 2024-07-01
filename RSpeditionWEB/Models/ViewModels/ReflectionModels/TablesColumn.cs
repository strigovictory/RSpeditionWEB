namespace RSpeditionWEB.Models.ViewModels.ReflectionModels
{
    // Модель, которая не имеет аналога в БД
    // Это характеристики столбца заданной таблицы 
    public class TablesColumn : ICloneable
    {
        [DisplayAttribute(Name = "Наименование UI")]
        public string DisplayName { get; set; } = string.Empty; // наименование столбца таблицы для отображения на UI   

        [DisplayAttribute(Name = "Наименование DB")]
        public string DBName { get; set; } = string.Empty;  // наименование столбца таблицы в БД --> по умолчанию наименования свойств класса модели не отличаются от наименования столбцов таблицы БД

        [DisplayAttribute(Name = "Идентификатор")]
        public bool IsIdentity { get; set; }

        [DisplayAttribute(Name = "Видимый")]
        public bool IsVisible { get; set; } = true;

        [DisplayAttribute(Name = "Только чтение")]
        public bool IsReadOnly { get; set; } = false;

        [DisplayAttribute(Name = "Фильтр")]
        public bool IsNeedFilter { get; set; } = false; // истина, если фильтр по этому столбцу должен устанавливаться

        [DisplayAttribute(Name = "Порядок следования")]
        public int SortOrder { get; set; } = 1; // порядок следования столбца в таблице

        [DisplayAttribute(Name = "Класс FK")]
        public string FK_ClassName { get; set; } = string.Empty;  // наименование вложенного класса, на который ссылается внешний ключ

        [DisplayAttribute(Name = "Свойство FK")]
        public string FK_PropNameSelected { get; set; } = string.Empty;  // наименование выбранного свойства во вложенном классе, значение которого будет отображаться

        [DisplayAttribute(Name = "Свойства FK")]
        [JsonIgnore]
        public List<string> FK_PropNames { get; set; } = new(); // наименования свойств во вложенном классе, на который ссылается внешний ключ

        public object Clone()
        => new TablesColumn 
        { 
            DisplayName = this.DisplayName,
            DBName = this.DBName,
            IsIdentity = this.IsIdentity,
            IsVisible = this.IsVisible,
            IsReadOnly = this.IsReadOnly,
            IsNeedFilter = this.IsNeedFilter,
            SortOrder = this.SortOrder,
            FK_ClassName = this.FK_ClassName,
            FK_PropNameSelected = this.FK_PropNameSelected,
            FK_PropNames = this.FK_PropNames,
        };
    }
}
