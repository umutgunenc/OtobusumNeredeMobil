using System.Xml.Serialization;

namespace OtobusumNerede.Shared.UIServices.ServicesDataModel
{
    [XmlRoot("Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class SoapEnvelope
    {
        [XmlElement("Body")]
        public SoapBody Body { get; set; }
    }
}
