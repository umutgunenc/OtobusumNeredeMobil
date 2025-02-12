using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;
using AutoMapper;
using OtobusumNerede.Api.Data.ServicesModels;
using OtobusumNerede.Api.Services.Interfaces;
using OtobusumNerede.Shared.DTOs;
using static System.Net.WebRequestMethods;

namespace OtobusumNerede.Api.Services.Classes
{
    public class HatDurakServices : IHatDurakServices
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        private List<HatDurakDto> _hatDurakDtoListesi;

        public HatDurakServices(HttpClient httpClient, IMapper mapper, List<HatDurakDto> hatDurakDtoListesi)
        {
            _httpClient = httpClient;
            _mapper = mapper;
            _hatDurakDtoListesi = hatDurakDtoListesi;
        }

        public async Task<List<HatDurakDto>> HattinDuraklariAsync(string hatKodu)
        {
            var url = "https://api.ibb.gov.tr/iett/ibb/ibb.asmx?";

            var soapRequest = $@"
                <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
                   <soapenv:Header/>
                   <soapenv:Body>
                      <tem:DurakDetay_GYY_wYonAdi>
                         <tem:hat_kodu>{hatKodu}</tem:hat_kodu>
                      </tem:DurakDetay_GYY_wYonAdi>
                   </soapenv:Body>
                </soapenv:Envelope>";

            var content = new StringContent(soapRequest, Encoding.UTF8, "text/xml");

            content.Headers.Add("SOAPAction", "http://tempuri.org/DurakDetay_GYY_wYonAdi");
            try
            {

                var response = await _httpClient.PostAsync(url, content);
                var responseString = await response.Content.ReadAsStringAsync();

                // Deserialize the XML response
                var serializer = new XmlSerializer(typeof(SoapEnvelope));
                var reader = new StringReader(responseString);
                var soapEnvelope = (SoapEnvelope)serializer.Deserialize(reader);

                // Get the list 
                var duraklar = soapEnvelope.Body.DurakDetayResponse.DurakDetayResult.HattinDuraklari.HatDurak;

                reader.Close();
                _hatDurakDtoListesi = _mapper.Map<List<HatDurakDto>>(duraklar);
                return _hatDurakDtoListesi;
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Hata oluştu: {ex.Message}");
                return null;
            }
        }
    }
}
