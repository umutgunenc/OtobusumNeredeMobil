using OtobusumNerede.Shared.ServicesDataModels;

namespace OtobusumNerede.Shared.Services.Interfaces
{
    public interface IKonumServices
    {

        Task<KonumResult> KonumBilgisiniAlAsync();

    }
}
