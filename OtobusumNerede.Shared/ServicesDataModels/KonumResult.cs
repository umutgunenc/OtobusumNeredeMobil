using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Devices.Sensors;

namespace OtobusumNerede.Shared.ServicesDataModels
{
    public class KonumResult
    {
        public bool IzinVarMi { get; set; }
        public Location? Konum { get; set; }
        public string? Mesaj { get; set; }

        public static KonumResult IzinVerildi(Location konum)
        {
            return new() { IzinVarMi = true, Konum = konum };
        }

        public static KonumResult IzinVerilmedi(string error)
        {
            return new() { IzinVarMi = false, Mesaj = error };
        }
    }
}
