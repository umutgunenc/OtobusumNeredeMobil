using OtobusumNerede.Api.Data.ServicesModels;
using OtobusumNerede.Shared.DTOs;

namespace OtobusumNerede.Api.Services.Interfaces
{
    public interface IHatOtobusServices
    {
        Task<List<HatOtobusDto>> HatOtobusBilgileriAsync(string hatKodu, HttpClient httpClient);
        Task<List<object>> HatGeoJsonBilgileriAsync (List<string> guzergahKodlari);

    }
}
