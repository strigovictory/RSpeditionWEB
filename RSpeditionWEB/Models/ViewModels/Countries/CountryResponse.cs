using Newtonsoft.Json;

namespace RSpeditionWEB.Models.ViewModels;

public class CountryResponse : IId
{
	[JsonInclude]
	public int Id { get; set; }
	
	[JsonInclude]
    public string Name { get; set; }

    [JsonInclude]
    public string NameEng { get; set; }

	[JsonInclude]
	public string Code { get; set; }

    [JsonInclude]
    public string NameOfficial { get; set; }

    [JsonInclude]
    public string NameOfficialEng { get; set; }
}
