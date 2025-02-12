using OtobusumNerede.Shared.DTOs;
using OtobusumNerede.Shared.Enums;
using Refit;

namespace OtobusumNerede.Web.Apis
{
    public interface IGetSeferSaatleriApi
    {
        [Get("/api/Sefer/GetSeferSaatleriByHatId/{hatKodu}/{gun}")]
        Task<SeferListeleriDto> GetSeferSaatiByIdAsync(string hatKodu, GunlerEnum gun);

    }
}
