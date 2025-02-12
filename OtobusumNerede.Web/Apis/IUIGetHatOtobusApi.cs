using OtobusumNerede.Shared.DTOs;
using OtobusumNerede.Shared.UIServicesModels;
using Refit;

namespace OtobusumNerede.Web.Apis
{
    public interface IUIGetHatOtobusApi
    {
        [Post("/api/WebHatOtobusController/GetHatOtobus")]
        Task<List<GetHatOtoKonumJsonResultServiceModel>> HatOtobusBilgileriAsync(string hatKodu);
    }
}
