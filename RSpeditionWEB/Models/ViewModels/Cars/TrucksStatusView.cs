namespace RSpeditionWEB.Models.ViewModels.Cars;

[CustomDisplayAttribute("Статус прицепа")]
public partial class TrucksStatusView : IId
{
    public TrucksStatusView()
    {
    }

    [JsonInclude]
    [JsonPropertyName(nameof(Id))]
    public int Id { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(Status))]
    public string Status { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(StatusImage))]
    public byte[] StatusImage { get; set; }
}
