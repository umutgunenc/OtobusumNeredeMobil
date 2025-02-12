using OtobusumNerede.Shared.DTOs;
using Refit;

namespace OtobusumNerede.Web.Apis
{
    public interface IGetHatDurakApi
    {
        [Get("/api/HatDurak/GetHatDurak/{hatKodu}")]
        Task<List<HatDurakDto>> GetHatDurakById(string hatKodu);
    }
}
