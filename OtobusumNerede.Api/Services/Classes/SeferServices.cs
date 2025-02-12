using OtobusumNerede.Api.Data.ServicesModels;
using OtobusumNerede.Api.Services.Interfaces;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Xml.Serialization;
using OtobusumNerede.Shared.Enums;
using OtobusumNerede.Shared.DTOs;
using AutoMapper;

namespace OtobusumNerede.Api.Services.Classes
{
    public class SeferServices : ISeferServices
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        private List<SeferSaatiDto> _seferSaatleri;


        public SeferServices(HttpClient httpClient, IMapper mapper, List<SeferSaatiDto> seferSaatleri)
        {
            _httpClient = httpClient;
            _mapper = mapper;
            _seferSaatleri = seferSaatleri;
        }

        public async Task<SeferListeleriDto> GidisDonusSeferleriniListele(string hatKodu, GunlerEnum gun)
        {
            _seferSaatleri = await GetSeferSaatiByHatIdAsync(hatKodu, gun);
            var GidisDonusListesi = new SeferListeleriDto();
            foreach (var seferler in _seferSaatleri)
            {
                if (seferler.SeferYonu == SeferYonu.Gidis)
                    GidisDonusListesi.GidisSeferleri.Add(seferler);
                else
                    GidisDonusListesi.DonusSeferleri.Add(seferler);
            }

            return GidisDonusListesi;
        }

        private async Task<List<SeferSaatiDto>> GetSeferSaatiByHatIdAsync(string hatKodu, GunlerEnum gun)
        {
            var url = "https://api.ibb.gov.tr/iett/UlasimAnaVeri/PlanlananSeferSaati.asmx";

            var soapRequest = $@"
                <soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:tem='http://tempuri.org/'>
                   <soapenv:Header/>
                   <soapenv:Body>
                      <tem:GetPlanlananSeferSaati_json>
                         <tem:HatKodu>{hatKodu}</tem:HatKodu>
                      </tem:GetPlanlananSeferSaati_json>
                   </soapenv:Body>
                </soapenv:Envelope>";

            var content = new StringContent(soapRequest, Encoding.UTF8, "text/xml");

            // İBB SOAPAction header
            content.Headers.Add("SOAPAction", "http://tempuri.org/GetPlanlananSeferSaati_json");

            try
            {

                var response = await _httpClient.PostAsync(url, content);
                var responseString = await response.Content.ReadAsStringAsync();

                // Deserialize the XML response
                var serializer = new XmlSerializer(typeof(SoapEnvelope));
                var reader = new StringReader(responseString);
                var soapEnvelope = (SoapEnvelope)serializer.Deserialize(reader);

                // Get the list of GetHat_json objects
                List<GetPlanlananSeferSaati_jsonServicesModel> seferSaatleri = JsonSerializer.Deserialize<List<GetPlanlananSeferSaati_jsonServicesModel>>(soapEnvelope.Body.GetPlanlananSeferSaatiJsonResponse.GetPlanlananSeferSaatiJsonResult);

                var secilenSeferSaatleri = _mapper.Map<List<SeferSaatiDto>>(seferSaatleri);
                reader.Close();

                _seferSaatleri = GetSecilenHattinSeferSaatiByGun(secilenSeferSaatleri, gun);
                return _seferSaatleri;

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Hata oluştu: {ex.Message}");
                return null;
            }
        }

        private List<SeferSaatiDto> GetSecilenHattinSeferSaatiByGun(List<SeferSaatiDto> secilenHattinSeferleri, GunlerEnum secilenGun)
        {
            string secilenGunTipi = "";
            switch (secilenGun)
            {
                case GunlerEnum.HaftaIci:
                    secilenGunTipi = "I";
                    break;
                case GunlerEnum.Cumartesi:
                    secilenGunTipi = "C";
                    break;
                case GunlerEnum.Pazar:
                    secilenGunTipi = "P";
                    break;
                default:
                    secilenGunTipi = "";
                    break;

            }
            return secilenHattinSeferleri.Where(x => x.GunTipi == secilenGunTipi).ToList();

        }


    }
}
