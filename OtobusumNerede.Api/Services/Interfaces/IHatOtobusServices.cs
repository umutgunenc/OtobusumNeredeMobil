using OtobusumNerede.Api.Data.ServicesModels;
using OtobusumNerede.Shared.DTOs;

namespace OtobusumNerede.Api.Services.Interfaces
{
    public interface IHatOtobusServices
    {
        Task<List<HatOtobusDto>> HatOtobusBilgileriAsync(List<GetHatOtoKonumJsonResultServiceModel> HatOtobusJsonServiceModel);
        Task<List<object>> HatGeoJsonBilgileriAsync (List<string> guzergahKodlari);

    }
}
