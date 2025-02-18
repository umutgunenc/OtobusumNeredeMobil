using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtobusumNerede.Shared.Services.Interfaces
{
    public interface IUyariServices
    {
        Task IzinUyarisiniGosterAsync(string mesaj);
        Task GpsUyarisiniGosterAsync(string mesaj);
    }
}
