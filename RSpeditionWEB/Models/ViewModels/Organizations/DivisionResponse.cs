namespace RSpeditionWEB.Models.ViewModels.Organizations;

[Display(Name = "Подразделение")]
public class DivisionResponse : IId
{
    public DivisionResponse() { }

    [JsonInclude]
    public int Id { get; set; }

    [CustomDisplayAttribute("Подразделение", " Укажите наименование подразделения")]
    [JsonInclude]
    public string Name { get; set; }

    [JsonInclude]
    public bool IsMainDivision { get; set; }

    [JsonInclude]
    public bool IsOnlyForwarding { get; set; }
}
