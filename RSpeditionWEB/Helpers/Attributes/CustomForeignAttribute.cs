namespace RSpeditionWEB.Helpers.Attributes;

// Класс пользовательского атрибута, который применяется к int-ым свойствам, которые по факту отмечены или нет в БД как внешние ключи (т.к. не все отмечены)

public class CustomForeignAttribute : System.Attribute
{
    // Тип класса, на который по факту ссылается ключ, помеченный или нет как внешний
    public Type TypeForeignClass { get; set; }

    // Наименование свойства вложенного класса, которое будет использоваться для рендеринга
    public string NamePropForeignClass { get; set; }

    public CustomForeignAttribute(Type type, string name)
    {
        this.TypeForeignClass = type;
        this.NamePropForeignClass = name;
    }
}
