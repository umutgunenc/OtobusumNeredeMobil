using OtobusumNerede.Shared.DTOs;
using Refit;

namespace OtobusumNerede.Shared.Apis
{
    public interface IGetHatDurakApi
    {
        [Get("/api/HatDurak/GetHatDurak/{hatKodu}")]
        Task<List<HatDurakDto>> GetHatDurakById(string hatKodu);
    }
}
