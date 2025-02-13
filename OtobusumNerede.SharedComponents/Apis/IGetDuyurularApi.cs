using OtobusumNerede.Shared.DTOs;
using Refit;

namespace OtobusumNerede.Shared.Apis
{
    public interface IGetDuyurularApi
    {
        [Get("/api/Duyurular/GetAllDuyurular")]
        Task<List<DuyurularDto>> GetDuyurularAsync();
    }
}
