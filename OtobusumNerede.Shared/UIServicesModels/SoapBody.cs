using System.Xml.Serialization;

namespace OtobusumNerede.Shared.UIServicesModels
{
    public class SoapBody
    {

        [XmlElement("GetHatOtoKonum_jsonResponse", Namespace = "http://tempuri.org/")]
        public GetHatOtoKonumJsonResponse GetHatOtoKonumJsonResponse { get; set; }

    }
}
