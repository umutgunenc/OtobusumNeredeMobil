using OtobusumNerede.Shared.DTOs;
using OtobusumNerede.Shared.Enums;
using Refit;

namespace OtobusumNerede.Web.Apis
{
    public interface IGetHatOtobusApi
    {
        //[Get("/api/HatOtobus/GetHatOtobus/{htppClient}")]
        //Task<List<HatOtobusDto>> GetHatOtobusById(string hatKodu);

        [Post("/api/HatOtobus/GetHatOtobus/{hatKodu}/{httpClient}")]
        Task<List<HatOtobusDto>> HatOtobusBilgileriAsync(string hatKodu, HttpClient httpClient);

        [Get("/api/HatOtobus/GetHatGeoJson")]
        Task<List<object>> GuzergahBilgileriAsync([Query(CollectionFormat.Multi)] List<string> guzergahKodlari);

    }
}
