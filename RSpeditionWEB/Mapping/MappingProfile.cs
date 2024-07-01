using RSpeditionWEB.Models.ViewModels.Banks;
using RSpeditionWEB.Models.ViewModels.Currencies;
using RSpeditionWEB.Models.ViewModels.Rides;

namespace RSpeditionWEB.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeCreditCardView, EmployeeCreditCard_View_GUID>().ReverseMap();
        }
    }
}
