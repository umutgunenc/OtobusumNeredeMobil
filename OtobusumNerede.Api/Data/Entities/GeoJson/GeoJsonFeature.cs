namespace OtobusumNerede.Api.Data.Entities.GeoJson
{
    public class GeoJsonFeature
    {
        public string Type { get; set; }
        public Properties Properties { get; set; }
        public Geometry Geometry { get; set; }
    }
}
