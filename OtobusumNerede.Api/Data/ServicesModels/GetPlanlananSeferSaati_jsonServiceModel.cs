using System.Xml.Serialization;

namespace OtobusumNerede.Api.Data.ServicesModels
{
    public class GetPlanlananSeferSaatiJsonResponse
    {
        [XmlElement("GetPlanlananSeferSaati_jsonResult")]
        public string GetPlanlananSeferSaatiJsonResult { get; set; } // JSON string
    }

    public class GetPlanlananSeferSaati_jsonServicesModel
    {
        public string SHATKODU { get; set; }
        public string HATADI { get; set; }
        public string SGUZERAH { get; set; }
        public string SYON { get; set; }
        public string SGUNTIPI { get; set; }
        public string GUZERGAH_ISARETI { get; set; }
        public string SSERVISTIPI { get; set; }
        public string DT { get; set; } 
    }
}
