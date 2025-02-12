using OtobusumNerede.Api.Data.Entities;
using OtobusumNerede.Shared.DTOs;

namespace OtobusumNerede.Api.Services.Interfaces
{
    public interface IHatServices
    {
        Task<List<HatDto>> GetAllHatDtoAsync();

    }
}
