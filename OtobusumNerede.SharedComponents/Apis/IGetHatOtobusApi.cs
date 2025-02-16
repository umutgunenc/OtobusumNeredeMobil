using OtobusumNerede.Shared.DTOs;
using OtobusumNerede.Shared.UIServices.ServicesDataModel;
using Refit;

namespace OtobusumNerede.Shared.Apis
{
    public interface IGetHatOtobusApi
    {


        //[Post("/api/HatOtobus/GetHatOtobus/{hatKodu}/{httpClient}")]
        //Task<List<HatOtobusDto>> HatOtobusBilgileriAsync(string hatKodu, HttpClient httpClient);

        [Get("/api/HatOtobus/GetHatGeoJson")]
        Task<List<object>> GuzergahBilgileriAsync([Query(CollectionFormat.Multi)] List<string> guzergahKodlari);

        [Post("/api/HatOtobus/GetHatOtobus")]
        Task<List<HatOtobusDto>> HatOtobusBilgileriAsync([Body] List<GetHatOtoKonumJsonResultServiceModel> model);


    }
}
