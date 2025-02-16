using OtobusumNerede.Shared.DTOs;
using OtobusumNerede.Shared.UIServices.ServicesDataModel;

namespace OtobusumNerede.Api.Services.Interfaces
{
    public interface IHatOtobusServices
    {
        //Task<List<HatOtobusDto>> HatOtobusBilgileriAsync(string hatKodu);
        List<HatOtobusDto> HatOtobusBilgileri(List<GetHatOtoKonumJsonResultServiceModel> model);

        Task<List<object>> HatGeoJsonBilgileriAsync (List<string> guzergahKodlari);



    }
}
