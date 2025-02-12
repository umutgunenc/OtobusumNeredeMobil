namespace OtobusumNerede.Api.Data.Entities.GeoJson
{
    public class GeoJsonRoot
    {
        public string Type { get; set; }
        public List<GeoJsonFeature> Features { get; set; }
    }
}
