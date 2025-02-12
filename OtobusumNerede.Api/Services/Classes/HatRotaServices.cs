using System.Diagnostics;
using Newtonsoft.Json;
using OtobusumNerede.Api.Data;
using OtobusumNerede.Api.Data.Entities.GeoJson;
using OtobusumNerede.Api.Services.Interfaces;

namespace OtobusumNerede.Api.Services.Classes
{
    public class HatRotaServices : IHatRotaServices
    {
        private readonly OtobusumNeredeDbContext _context;

        public HatRotaServices(OtobusumNeredeDbContext context)
        {
            _context = context;
        }

        public async Task UpdateHatRotalariAsync()
        {
            string jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "GeoJson", "iett.geojson");
            string jsonContent = File.ReadAllText(jsonFilePath);

            var geoJsonData = JsonConvert.DeserializeObject<GeoJsonRoot>(jsonContent);
            int sayac =0;
            try
            {
                foreach (var item in geoJsonData.Features)
                {
                    string wktGeometry = ConvertToWkt(item.Geometry);
                    var hatRota = new OtobusRotasi
                    {
                        ID = item.Properties.ID,
                        GUZERGAH_A = item.Properties.GUZERGAH_A,
                        GUZERGAH_K = item.Properties.GUZERGAH_K,
                        HAT_ADI = item.Properties.HAT_ADI,
                        HAT_KODU = item.Properties.HAT_KODU,
                        DEPAR_NO = item.Properties.DEPAR_NO,
                        DURUM = item.Properties.DURUM,
                        UZUNLUK = item.Properties.UZUNLUK,
                        HAT_BASI = item.Properties.HAT_BASI,
                        HAT_SONU = item.Properties.HAT_SONU,
                        YON = item.Properties.YON,
                        HAT_ID = item.Properties.HAT_ID,
                        SURE = item.Properties.SURE,
                        RING_MI = item.Properties.RING_MI,
                        B_NOKTASI = item.Properties.B_NOKTASI,
                        RouteGeometry = wktGeometry
                    };
                    await _context.OtobusRotalari.AddAsync(hatRota);
                    sayac++;
                    Console.WriteLine(sayac);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            

        }

        private string ConvertToWkt(Geometry geometry)
        {
            if (geometry.Type != "MultiLineString")
                throw new NotSupportedException("Sadece MultiLineString desteklenmektedir.");

            var wkt = "MULTILINESTRING(";
            foreach (var line in geometry.Coordinates)
            {
                wkt += "(";
                foreach (var point in line)
                {
                    wkt += $"{point[0]} {point[1]},";
                }
                wkt = wkt.TrimEnd(',') + "),";
            }
            wkt = wkt.TrimEnd(',') + ")";
            return wkt;
        }
    }
}


