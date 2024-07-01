namespace RSpeditionWEB.Helpers.Attributes;

public class CustomDisplayAttribute : System.Attribute
{
    // Значение для отображения пользователю наименования свойства модели
    public string DisplayName { get; set; }

    // Значение для отображения пользователю наименования свойства модели
    public string RequiredMessage { get; set; }

    public List<string> CustomDisplayAttributes { get; set; } = new();

    public CustomDisplayAttribute(string name, string mess)
    {
        this.DisplayName = name;
        this.RequiredMessage = mess;
        this.CustomDisplayAttributes.AddRange(new List<string>{ name, mess});
    }
    public CustomDisplayAttribute(string name)
    {
        CustomDisplayAttributes.Add(name);
    }

    public CustomDisplayAttribute(params string[] data)
    {
        this.CustomDisplayAttributes.AddRange(data);
    }
}
