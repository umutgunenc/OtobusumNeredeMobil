using OtobusumNerede.Shared.DTOs;
using Refit;

namespace OtobusumNerede.Web.Apis
{
    public interface IGetHatlarApi
    {
        [Get("/api/Hat/GetAllHatId")]
        Task<List<HatDto>> GetHatlarDtoAsync();
    }
}
