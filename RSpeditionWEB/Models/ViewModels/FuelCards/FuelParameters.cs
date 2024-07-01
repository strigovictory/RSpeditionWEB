using RSpeditionWEB.Models.RequestModels;

namespace RSpeditionWEB.Models.ViewModels.FuelCards;

public class FuelParameters : RequestBase
{
    public FuelParameters(int providerId, bool isMonthly, string userName)
    : base(userName)
    {
        ProviderId = providerId;
        IsMonthly = isMonthly;
    }

    [JsonInclude]
    public int ProviderId { get; set; }

    [JsonInclude]
    public bool IsMonthly { get; set; }
}