using System.Xml.Serialization;

namespace OtobusumNerede.Shared.UIServices.ServicesDataModel
{
    public class GetHatOtoKonumJsonResponse
    {
        [XmlElement("GetHatOtoKonum_jsonResult")]
        public string GetHatOtoKonumJsonResult { get; set; }
    }

    public class GetHatOtoKonumJsonResultServiceModel
    {

        public string kapino { get; set; }
        public string boylam { get; set; }
        public string enlem { get; set; }
        public string hatkodu { get; set; }
        public string guzergahkodu { get; set; }
        public string hatad { get; set; }
        public string yon { get; set; }
        public string son_konum_zamani { get; set; }
        public string yakinDurakKodu { get; set; }
    }


}





