using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OtobusumNerede.Api.Data;
using OtobusumNerede.Api.Data.Entities.GeoJson;
using OtobusumNerede.Api.Data.ServicesModels;
using OtobusumNerede.Api.Services.Interfaces;
using OtobusumNerede.Shared.DTOs;

namespace OtobusumNerede.Api.Services
{
    public class HatOtobusServices : IHatOtobusServices
    {
        private HttpClient _httpClient;
        private List<HatOtobusDto> _hatOtobusDtoList;
        private readonly IMapper _mapper;
        private readonly OtobusumNeredeDbContext _context;

        public HatOtobusServices( List<HatOtobusDto> hatOtobusDto, IMapper mapper, OtobusumNeredeDbContext context)
        {
            _hatOtobusDtoList = hatOtobusDto;
            _mapper = mapper;
            _context = context;
        }


        public async Task<List<HatOtobusDto>> HatOtobusBilgileriAsync(List<GetHatOtoKonumJsonResultServiceModel> HatOtobusJsonServiceModel)
        {
            //var url = "https://api.ibb.gov.tr/iett/FiloDurum/SeferGerceklesme.asmx?wsdl";

            //var soapRequest = $@"
            //    <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
            //       <soapenv:Header/>
            //       <soapenv:Body>
            //          <tem:GetHatOtoKonum_json>
            //             <!--Optional:-->
            //             <tem:HatKodu>{hatKodu}</tem:HatKodu>
            //          </tem:GetHatOtoKonum_json>
            //       </soapenv:Body>
            //    </soapenv:Envelope>";

            //var content = new StringContent(soapRequest, Encoding.UTF8, "text/xml");

            //content.Headers.Add("SOAPAction", "http://tempuri.org/GetHatOtoKonum_json");
            try
            {

                //    var response = await _httpClient.PostAsync(url, content);
                //    var responseString = await response.Content.ReadAsStringAsync();

                //    // Deserialize the XML response
                //    var serializer = new XmlSerializer(typeof(SoapEnvelope));
                //    var reader = new StringReader(responseString);
                //    var soapEnvelope = (SoapEnvelope)serializer.Deserialize(reader);


                //    var HatOtobusJsonServiceModel = JsonSerializer.Deserialize<List<GetHatOtoKonumJsonResultServiceModel>>(soapEnvelope.Body.GetHatOtoKonumJsonResponse.GetHatOtoKonumJsonResult);

                _hatOtobusDtoList = _mapper.Map<List<HatOtobusDto>>(HatOtobusJsonServiceModel);
                //reader.Close();


                return _hatOtobusDtoList;

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Hata oluştu: {ex.Message}");
                return null;
            }
        }

        public async Task<List<object>> HatGeoJsonBilgileriAsync(List<string> guzergahKodlari)
        {
            var query = _context.OtobusRotalari
                .Where(x => guzergahKodlari.Contains(x.GUZERGAH_K) && x.DURUM == "AKTİF")
                .Select(x => new OtobusRotasi
                {
                    RouteGeometry = x.RouteGeometry,
                    YON = x.YON,
                    DEPAR_NO = x.DEPAR_NO
                });

            var hatGeoJsonList = await query.ToListAsync();

            if (hatGeoJsonList.Count > 0)
            {
                List<object> GeoJson = new();  // Burada List<string> yerine List<object> kullandık
                foreach (var geoJson in hatGeoJsonList)
                {
                    GeoJson.Add(ConvertToGeoJson(geoJson));  // JSON nesnesi direkt ekleniyor
                }

                return GeoJson;  // JSON nesneleri olarak döndürülüyor
            }

            return null;
        }



        private object ConvertToGeoJson(OtobusRotasi rota)
        {
            // "MULTILINESTRING((" ve "))" kısımlarını kaldır
            var trimmed = rota.RouteGeometry.Replace("MULTILINESTRING((", "").Replace("))", "");

            // Noktaları ayır
            var points = trimmed.Split(',');

            // Koordinatları listeye ekle
            var coordinates = new List<List<double>>();

            foreach (var point in points)
            {
                var parts = point.Trim().Split(' ');
                if (parts.Length == 2)
                {
                    double longitude = double.Parse(parts[0], CultureInfo.InvariantCulture);
                    double latitude = double.Parse(parts[1], CultureInfo.InvariantCulture);

                    coordinates.Add(new List<double> { longitude, latitude });
                }
            }

            // GeoJSON yapısı oluştur
            var geoJsonObject = new
            {
                type = "Feature",
                geometry = new
                {
                    type = "MultiLineString",
                    coordinates = new List<List<List<double>>> { coordinates }
                },
                properties = new
                {
                    Yon = rota.YON,
                    DeparNo = rota.DEPAR_NO
                }
            };

            return geoJsonObject;

            //// GeoJSON string olarak döndür
            //return JsonSerializer.Serialize(geoJsonObject, new JsonSerializerOptions
            //{
            //    WriteIndented = true, // JSON'u daha okunabilir hale getirir
            //    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping // Kaçış karakterlerini kaldırır
            //});
        }
    }

}