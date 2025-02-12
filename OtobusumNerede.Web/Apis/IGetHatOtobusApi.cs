using Microsoft.AspNetCore.Mvc;
using OtobusumNerede.Shared.DTOs;
using OtobusumNerede.Shared.Enums;
using OtobusumNerede.Shared.UIServicesModels;
using Refit;

namespace OtobusumNerede.Web.Apis
{
    public interface IGetHatOtobusApi
    {
        //[Get("/api/HatOtobus/GetHatOtobus/{htppClient}")]
        //Task<List<HatOtobusDto>> GetHatOtobusById(string hatKodu);

        [Get("/api/HatOtobus/GetHatGeoJson")]
        Task<List<object>> GuzergahBilgileriAsync([Query(CollectionFormat.Multi)] List<string> guzergahKodlari);

        [Post("/api/HatOtobus/GetHatOtobus")]
        Task<List<HatOtobusDto>> GetHatOtobusDtoAsync (List<GetHatOtoKonumJsonResultServiceModel> model);

    }
}
