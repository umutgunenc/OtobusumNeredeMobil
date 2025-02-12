using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OtobusumNerede.Shared.Enums;

namespace OtobusumNerede.Shared.DTOs
{
    public class HatDurakDto
    {
        public string HatKodu { get; set; }
        public SeferYonu SeferYonu { get; set; }
        public string GidisYonAdi { get; set; }
        public int SiraNo { get; set; }
        public string DurakKodu { get; set; }
        public string DurakAdi { get; set; }
        public double Enlem { get; set; }
        public double Boylam { get; set; }

    }
}
