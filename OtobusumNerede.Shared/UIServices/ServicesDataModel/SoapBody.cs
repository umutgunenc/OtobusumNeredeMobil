using System.Xml.Serialization;

namespace OtobusumNerede.Shared.UIServices.ServicesDataModel
{
    public class SoapBody
    {

        [XmlElement("GetHatOtoKonum_jsonResponse", Namespace = "http://tempuri.org/")]
        public GetHatOtoKonumJsonResponse GetHatOtoKonumJsonResponse { get; set; }

    }
}
