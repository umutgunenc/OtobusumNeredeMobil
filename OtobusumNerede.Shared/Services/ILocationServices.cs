using OtobusumNerede.Shared.ServicesDataModels;

namespace OtobusumNerede.Shared.Services
{
    public interface ILocationServices
    {

        Task<LocationResult> KonumBilgisiniAlAsync();

    }
}
