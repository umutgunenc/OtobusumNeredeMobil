using System.Xml.Serialization;

namespace OtobusumNerede.Api.Data.ServicesModels
{
    public class SoapBody
    {
        [XmlElement("GetHat_jsonResponse", Namespace = "http://tempuri.org/")]
        public GetHatJsonResponse GetHatJsonResponse { get; set; }


        [XmlElement("GetDurak_jsonResponse", Namespace = "http://tempuri.org/")]
        public GetDurakJsonResponse GetDurakJsonResponse { get; set; }

        [XmlElement("GetPlanlananSeferSaati_jsonResponse", Namespace = "http://tempuri.org/")]
        public GetPlanlananSeferSaatiJsonResponse GetPlanlananSeferSaatiJsonResponse { get; set; }


        [XmlElement(ElementName = "DurakDetay_GYY_wYonAdiResponse", Namespace = "http://tempuri.org/")]
        public DurakDetayResponse DurakDetayResponse { get; set; }

        [XmlElement("GetHatOtoKonum_jsonResponse", Namespace = "http://tempuri.org/")]
        public GetHatOtoKonumJsonResponse GetHatOtoKonumJsonResponse { get; set; }

    }
}
