namespace OtobusumNerede.Api.Data.Entities.GeoJson
{
    public class Geometry
    {
        public string Type { get; set; }
        public List<List<List<double>>> Coordinates { get; set; } // MultiLineString için

    }
}
