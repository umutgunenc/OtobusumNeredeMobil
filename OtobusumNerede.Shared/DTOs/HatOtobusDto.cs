using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OtobusumNerede.Shared.DTOs
{
    public class HatOtobusDto
    {
        public string KapiNo { get; set; }

        public double Boylam { get; set; }

        public double Enlem { get; set; }

        public string HatKodu { get; set; }
        public string GuzergahKodu { get; set; }

        public string YonAdi { get; set; }

        public DateTime KonumZamani { get; set; }
    }
}
