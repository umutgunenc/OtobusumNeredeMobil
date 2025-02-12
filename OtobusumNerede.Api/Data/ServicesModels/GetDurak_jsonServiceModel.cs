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
    //    [XmlElement("GetDurak_jsonResponse", Namespace = "http://tempuri.org/")]
    //    public GetDurakJsonResponse GetDurakJsonResponse { get; set; }
    //}

    public class GetDurakJsonResponse
    {
        [XmlElement("GetDurak_jsonResult")]
        public string GetDurakJsonResult { get; set; }
    }

    // Representing the Durak information
    public class GetDurak_jsonServicesModel
    {
        public int SDURAKKODU { get; set; }
        public string SDURAKADI { get; set; }
        public string KOORDINAT { get; set; }
        public string ILCEADI { get; set; }
        public string SYON { get; set; }
        public string AKILLI { get; set; }
        public string FIZIKI { get; set; }
        public string DURAK_TIPI { get; set; }
        public string ENGELLIKULLANIM { get; set; }
    }
}
