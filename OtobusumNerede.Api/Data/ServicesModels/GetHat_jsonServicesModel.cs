using System.Xml.Serialization;

namespace OtobusumNerede.Api.Data.ServicesModels
{
    //// Representing the SOAP Envelope
    //[XmlRoot("Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    //public class SoapEnvelope
    //{
    //    [XmlElement("Body")]
    //    public SoapBody Body { get; set; }
    //}

    //public class SoapBody
    //{
    //    [XmlElement("GetHat_jsonResponse", Namespace = "http://tempuri.org/")]
    //    public GetHatJsonResponse GetHatJsonResponse { get; set; }
    //}

    public class GetHatJsonResponse
    {
        [XmlElement("GetHat_jsonResult")]
        public string GetHatJsonResult { get; set; } // This is a JSON string
    }

    // Representing the Hat information
    public class GetHat_jsonServicesModel
    {
        public string SHATKODU { get; set; }
        public string SHATADI { get; set; }
        public string TARIFE { get; set; }
        public double HAT_UZUNLUGU { get; set; }
        public double SEFER_SURESI { get; set; }
    }
}
