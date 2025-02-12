using System.Xml.Serialization;

namespace OtobusumNerede.Api.Data.ServicesModels
{
    public class DurakDetayResponse
    {
        [XmlElement(ElementName = "DurakDetay_GYY_wYonAdiResult")]
        public DurakDetayResult DurakDetayResult { get; set; }
    }

    public class DurakDetayResult
    {
        [XmlElement(ElementName = "NewDataSet", Namespace = "")]
        public NewDataSet HattinDuraklari { get; set; }
    }

    [XmlRoot("NewDataSet")]
    public class NewDataSet
    {
        [XmlElement("Table")]
        public List<Table> HatDurak { get; set; }
    }

    public class Table
    {
        [XmlElement("HATKODU")]
        public string HatKodu { get; set; }

        [XmlElement("YON")]
        public string Yon { get; set; }

        [XmlElement("YON_ADI")]
        public string YonAdi { get; set; }

        [XmlElement("SIRANO")]
        public int SiraNo { get; set; }

        [XmlElement("DURAKKODU")]
        public string DurakKodu { get; set; }

        [XmlElement("DURAKADI")]
        public string DurakAdi { get; set; }

        [XmlElement("XKOORDINATI")]
        public double XKoordinati { get; set; }

        [XmlElement("YKOORDINATI")]
        public double YKoordinati { get; set; }

        [XmlElement("DURAKTIPI")]
        public string DurakTipi { get; set; }

        [XmlElement("ISLETMEBOLGE")]
        public string IsletmeBolge { get; set; }

        [XmlElement("ISLETMEALTBOLGE")]
        public string IsletmeAltBolge { get; set; }

        [XmlElement("ILCEADI")]
        public string IlceAdi { get; set; }
    }
}

