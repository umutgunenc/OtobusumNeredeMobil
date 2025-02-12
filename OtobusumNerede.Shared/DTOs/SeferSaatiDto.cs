using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OtobusumNerede.Shared.Enums;

namespace OtobusumNerede.Shared.DTOs
{
    public class SeferSaatiDto
    {
        public string HatKodu { get; set; }
        public string HatAdi { get; set; }
        public string BaslangıcDuragi { get; set; }
        public SeferYonu SeferYonu { get; set; }
        public string SeferSaati { get; set; }
        public string GunTipi { get; set; }
        public string GuzergahKodu { get; set; }
    }

    public class SeferListeleriDto
    {
        public List<SeferSaatiDto> GidisSeferleri { get; set; } = new List<SeferSaatiDto>();
        public List<SeferSaatiDto> DonusSeferleri { get; set; } = new List<SeferSaatiDto>();
    }
}
