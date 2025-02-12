using System.Xml.Serialization;

namespace OtobusumNerede.Api.Data.ServicesModels
{
    [XmlRoot("Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class SoapEnvelope
    {
        [XmlElement("Body")]
        public SoapBody Body { get; set; }
    }
}
