using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.ApplicationModel;

namespace OtobusumNerede.Shared.Services.Interfaces
{
    public interface IIzinServices
    {
        Task<bool> KonumIzinleriniKontrolEtAsync();
        Task KonumIzniIsteAsync();
        //Task<bool> IzinVeridiMiAsync();
    }


}
