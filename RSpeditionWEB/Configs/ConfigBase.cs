using Microsoft.Extensions.Options;

namespace RSpeditionWEB.Configs;

public abstract class ConfigBase
{
    public string BaseUrl { get; set; }

    public string Key { get; set; }
}
