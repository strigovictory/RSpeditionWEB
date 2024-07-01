using Newtonsoft.Json;

namespace RSpeditionWEB.Models.ViewModels.Cars;

[Display(Name ="Автомобиль")]
public class TruckResponse : IId
{
    public TruckResponse()
    { }

    [Required(ErrorMessage = "Задайте идентификатор автомобиля")]
    [CustomDisplayAttribute("Идентификатор автомобиля", "Задайте идентификатор автомобиля")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Укажите номер автомобиля")]
    [CustomDisplayAttribute("Номер автомобиля", "Укажите номер автомобиля")]
    [JsonPropertyName("licensePlate")]
    [JsonProperty("licensePlate")]
    public string CarNumber { get; set; }

    [JsonInclude]
    public bool IsDisabled { get; set; }

    [JsonInclude]
    public int? DivisionId { get; set; }

    public override string ToString()
    => $"Тягач гос.№: {this.CarNumber ?? String.Empty} ";
}