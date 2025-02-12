using OtobusumNerede.Shared.DTOs;
using Refit;

namespace OtobusumNerede.Web.Apis
{
    public interface IGetDuyurularApi
    {
        [Get("/api/Duyurular/GetAllDuyurular")]
        Task<List<DuyurularDto>> GetDuyurularAsync();
    }
}
