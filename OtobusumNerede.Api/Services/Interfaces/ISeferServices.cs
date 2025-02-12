using OtobusumNerede.Api.Data.ServicesModels;
using OtobusumNerede.Shared.DTOs;
using OtobusumNerede.Shared.Enums;

namespace OtobusumNerede.Api.Services.Interfaces
{
    public interface ISeferServices
    {

        Task<SeferListeleriDto> GidisDonusSeferleriniListele(string hatKodu, GunlerEnum gun);


    }
}
