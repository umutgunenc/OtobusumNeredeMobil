using OtobusumNerede.Shared.DTOs;

namespace OtobusumNerede.Api.Services.Interfaces
{
    public interface IIettDuyurularServices
    {
        Task<List<DuyurularDto>> GetDuyurularAsync();
    }
}
