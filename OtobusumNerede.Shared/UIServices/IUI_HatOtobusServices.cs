using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OtobusumNerede.Shared.UIServicesModels;

namespace OtobusumNerede.Shared.UIServices
{
    public interface IUI_HatOtobusServices
    {
        Task<List<GetHatOtoKonumJsonResultServiceModel>> HatOtobusBilgileriAsync(string hatKodu);
    }
}
