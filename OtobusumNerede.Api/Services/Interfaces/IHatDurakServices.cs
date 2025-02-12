using OtobusumNerede.Api.Data.ServicesModels;
using OtobusumNerede.Shared.DTOs;

namespace OtobusumNerede.Api.Services.Interfaces
{
    public interface IHatDurakServices
    {
        Task<List<HatDurakDto>> HattinDuraklariAsync(string HatKodu);
    }
}
