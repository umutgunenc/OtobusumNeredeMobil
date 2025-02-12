using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OtobusumNerede.Api.Data.Entities.GeoJson
{
    public class OtobusRotasi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public string? GUZERGAH_A { get; set; }
        public string? GUZERGAH_K { get; set; }
        public string? HAT_ADI { get; set; }
        public string? HAT_KODU { get; set; }
        public int? DEPAR_NO { get; set; }
        public string? DURUM { get; set; }
        public double? UZUNLUK { get; set; }
        public string? HAT_BASI { get; set; }
        public string? HAT_SONU { get; set; }
        public string? YON { get; set; }
        public int? HAT_ID { get; set; }
        public double? SURE { get; set; }
        public string? RING_MI { get; set; }
        public int? B_NOKTASI { get; set; }
        public string? RouteGeometry { get; set; } // WKT formatında saklanacak
    }
}
