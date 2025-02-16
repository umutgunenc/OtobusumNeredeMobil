using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OtobusumNerede.Shared.UIServices.ServicesDataModel;

namespace OtobusumNerede.Shared.UIServices.Services.Interfaces
{
    public interface IHatOtobusServices
    {
        Task<List<GetHatOtoKonumJsonResultServiceModel>> HatOtobusBilgileriAsync(string hatKodu);
    }
}
