using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using OtobusumNerede.Shared.DTOs;
using System.Xml.Serialization;
using OtobusumNerede.Shared.UIServicesModels;

namespace OtobusumNerede.Shared.UIServices
{
    public class UI_HatOtobusServices : IUI_HatOtobusServices
    {
        private HttpClient _httpClient;

        public UI_HatOtobusServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<GetHatOtoKonumJsonResultServiceModel>> HatOtobusBilgileriAsync(string hatKodu)
        {
            var url = "https://api.ibb.gov.tr/iett/FiloDurum/SeferGerceklesme.asmx?wsdl";

            var soapRequest = $@"
                <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
                   <soapenv:Header/>
                   <soapenv:Body>
                      <tem:GetHatOtoKonum_json>
                         <!--Optional:-->
                         <tem:HatKodu>{hatKodu}</tem:HatKodu>
                      </tem:GetHatOtoKonum_json>
                   </soapenv:Body>
                </soapenv:Envelope>";

            var content = new StringContent(soapRequest, Encoding.UTF8, "text/xml");

            content.Headers.Add("SOAPAction", "http://tempuri.org/GetHatOtoKonum_json");
            try
            {

                var response = await _httpClient.PostAsync(url, content);
                var responseString = await response.Content.ReadAsStringAsync();

                // Deserialize the XML response
                var serializer = new XmlSerializer(typeof(SoapEnvelope));
                var reader = new StringReader(responseString);
                var soapEnvelope = (SoapEnvelope)serializer.Deserialize(reader);


                var HatOtobusJsonServiceModel = JsonSerializer.Deserialize<List<GetHatOtoKonumJsonResultServiceModel>>(soapEnvelope.Body.GetHatOtoKonumJsonResponse.GetHatOtoKonumJsonResult);

                reader.Close();

                return HatOtobusJsonServiceModel;
            }

            catch (Exception ex)
            {
                return null;
            }
        }
    }
}