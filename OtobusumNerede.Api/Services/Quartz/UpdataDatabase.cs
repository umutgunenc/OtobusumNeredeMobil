using OtobusumNerede.Api.Services.Interfaces;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Xml.Serialization;
using OtobusumNerede.Api.Data;
using Microsoft.EntityFrameworkCore;
using OtobusumNerede.Api.Data.Entities;
using OtobusumNerede.Api.Data.ServicesModels;
using Microsoft.AspNetCore.Server.HttpSys;
using System.Globalization;
using System;

namespace OtobusumNerede.Api.Services.Quartz
{
    public class UpdataDatabase : IUpdateDatabase
    {
        private readonly HttpClient _httpClient;
        private readonly OtobusumNeredeDbContext _dbContext;
        public UpdataDatabase(HttpClient httpClient, OtobusumNeredeDbContext dbContext)
        {
            _httpClient = httpClient;
            _dbContext = dbContext;
        }
        public Task UpdateGunlerAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateHatlarAsync()
        {

            // İBB SOAP endpoint URL
            var url = "https://api.ibb.gov.tr/iett/UlasimAnaVeri/HatDurakGuzergah.asmx";

            var soapRequest = @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
                            <soapenv:Header/>
                            <soapenv:Body>
                                <tem:GetHat_json>
                                    <tem:HatKodu></tem:HatKodu>
                                </tem:GetHat_json>
                            </soapenv:Body>
                        </soapenv:Envelope>";

            var content = new StringContent(soapRequest, Encoding.UTF8, "text/xml");

            // İBB SOAPAction header
            content.Headers.Add("SOAPAction", "http://tempuri.org/GetHat_json");

            try
            {

                var response = await _httpClient.PostAsync(url, content);
                var responseString = await response.Content.ReadAsStringAsync();

                // Deserialize the XML response
                var serializer = new XmlSerializer(typeof(SoapEnvelope));
                var reader = new StringReader(responseString);
                var soapEnvelope = (SoapEnvelope)serializer.Deserialize(reader);

                // Get the list of GetHat_json objects
                List<GetHat_jsonServicesModel> hatListesi = JsonSerializer.Deserialize<List<GetHat_jsonServicesModel>>(soapEnvelope.Body.GetHatJsonResponse.GetHatJsonResult);

                reader.Close();


                await UpdateDatabaseHatlarAsync(hatListesi);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Hata oluştu: {ex.Message}");
            }


        }

        public async Task UpdateDuraklarAsync()
        {
            // İBB SOAP endpoint URL
            var url = "https://api.ibb.gov.tr/iett/UlasimAnaVeri/HatDurakGuzergah.asmx";

            var soapRequest = @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
                           <soapenv:Header/>
                           <soapenv:Body>
                              <tem:GetDurak_json>
                                 <tem:DurakKodu></tem:DurakKodu>
                              </tem:GetDurak_json>
                           </soapenv:Body>
                        </soapenv:Envelope>";

            var content = new StringContent(soapRequest, Encoding.UTF8, "text/xml");

            // İBB SOAPAction header
            content.Headers.Add("SOAPAction", "http://tempuri.org/GetDurak_json");

            try
            {

                var response = await _httpClient.PostAsync(url, content);
                var responseString = await response.Content.ReadAsStringAsync();

                // Deserialize the XML response
                var serializer = new XmlSerializer(typeof(SoapEnvelope));
                var reader = new StringReader(responseString);
                var soapEnvelope = (SoapEnvelope)serializer.Deserialize(reader);

                // Get the list of GetHat_json objects
                List<GetDurak_jsonServicesModel> durakListesi = JsonSerializer.Deserialize<List<GetDurak_jsonServicesModel>>(soapEnvelope.Body.GetDurakJsonResponse.GetDurakJsonResult);

                reader.Close();


                await UpdateDatabaseDuraklarAsync(durakListesi);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Hata oluştu: {ex.Message}");
            }
        }





        private async Task UpdateDatabaseDuraklarAsync(List<GetDurak_jsonServicesModel>? durakListesi)
        {

            foreach (var durak in durakListesi)
            {
                var kayitliDurak = await _dbContext.Duraklar.FirstOrDefaultAsync(d => d.Id == durak.SDURAKKODU);
                var koordinatlar = ParseKoordinat(durak.KOORDINAT);
                var akilliMi = ParseAkilliMi(durak.AKILLI);
                var engelliKullaniminaUygunMu = ParseEngelliKullanimiUygunMu(durak.ENGELLIKULLANIM);

                if (kayitliDurak == null)
                {

                    var newDurak = new Durak
                    {
                        DurakKodu = durak.SDURAKKODU,
                        Enlem = koordinatlar.Enlem,
                        Boylam = koordinatlar.Boylam,
                        Adi = durak.SDURAKADI,
                        EngelliKullaniminaUygunMu = engelliKullaniminaUygunMu,
                        AkilliMi = akilliMi,
                        IlceAdi = durak.ILCEADI
                    };
                    await _dbContext.Duraklar.AddAsync(newDurak);
                }
                else
                {
                    if (DurakBilgileriDegismisMi(kayitliDurak, durak, koordinatlar, akilliMi, engelliKullaniminaUygunMu))
                    {
                        kayitliDurak.DurakKodu = durak.SDURAKKODU;
                        kayitliDurak.Enlem = koordinatlar.Enlem;
                        kayitliDurak.Boylam = koordinatlar.Boylam;
                        kayitliDurak.Adi = durak.SDURAKADI;
                        kayitliDurak.EngelliKullaniminaUygunMu = engelliKullaniminaUygunMu;
                        kayitliDurak.AkilliMi = akilliMi;
                        kayitliDurak.IlceAdi = durak.ILCEADI;

                        _dbContext.Duraklar.Update(kayitliDurak);

                    }

                }
            }

            var aktifDuraklarinIdleri = durakListesi.Select(h => h.SDURAKKODU).ToList();
            var aktifOlmayanDuraklarinIdleriQuery = _dbContext.Duraklar.Where(h => !aktifDuraklarinIdleri.Contains(h.DurakKodu));

            var aktifOlmayanDuraklarinIdleri = await aktifOlmayanDuraklarinIdleriQuery.ToListAsync();

            if (aktifOlmayanDuraklarinIdleri.Any())
                _dbContext.Duraklar.RemoveRange(aktifOlmayanDuraklarinIdleri);

            await _dbContext.SaveChangesAsync();


        }

        private async Task UpdateDatabaseHatlarAsync(List<GetHat_jsonServicesModel> aktifHatlar)
        {
            foreach (var aktifHat in aktifHatlar)
            {
                var kayitliHat = await _dbContext.Hatlar.FirstOrDefaultAsync(h => h.Id == aktifHat.SHATKODU);

                if (kayitliHat == null)
                {
                    var newHat = new Hat
                    {
                        Id = aktifHat.SHATKODU,
                        HatAdi = aktifHat.SHATADI,
                    };
                    await _dbContext.Hatlar.AddAsync(newHat);
                }
                else
                {
                    if (kayitliHat.HatAdi != aktifHat.SHATADI)
                    {
                        kayitliHat.HatAdi = aktifHat.SHATADI;
                        _dbContext.Hatlar.Update(kayitliHat);
                    }
                }
            }

            var aktifHatlarinIdleri = aktifHatlar.Select(h => h.SHATKODU).ToList();
            var aktifOlmayanHatlarinIdleriQuery = _dbContext.Hatlar.Where(h => !aktifHatlarinIdleri.Contains(h.Id));

            var aktifOlmayanHatlarinIdleri = await aktifOlmayanHatlarinIdleriQuery.ToListAsync();

            if (aktifOlmayanHatlarinIdleri.Any())
                _dbContext.Hatlar.RemoveRange(aktifOlmayanHatlarinIdleri);

            await _dbContext.SaveChangesAsync();


        }

        private (double Boylam, double Enlem) ParseKoordinat(string koordinat)
        {
            var parts = koordinat.Replace("POINT (", "").Replace(")", "").Split(' ');

            // Boylam ve Enlem değerlerini parse etmek
            double boylam = double.Parse(parts[0], CultureInfo.InvariantCulture);
            double enlem = double.Parse(parts[1], CultureInfo.InvariantCulture);

            return (boylam, enlem);
        }

        private bool ParseAkilliMi(string AkilliMi)
        {
            if (string.IsNullOrEmpty(AkilliMi))
                return false;
            if (AkilliMi == "YOK")
                return false;
            return true;
        }

        private bool ParseEngelliKullanimiUygunMu(string EngelliKullaniminaUygunMu)
        {
            if (string.IsNullOrEmpty(EngelliKullaniminaUygunMu))
                return false;
            if (EngelliKullaniminaUygunMu == "Uygun Degil")
                return false;
            return true;
        }

        private bool DurakBilgileriDegismisMi(Durak durak, GetDurak_jsonServicesModel jsonDurak, (double Boylam, double Enlem) koordinatlar, bool akilliMi, bool engelliKullaniminaUygunMu)
        {
            if (durak.DurakKodu != jsonDurak.SDURAKKODU)
                return false;
            if (durak.Enlem != koordinatlar.Enlem)
                return false;
            if (durak.Boylam != koordinatlar.Boylam)
                return false;
            if (durak.AkilliMi != akilliMi)
                return false;
            if (durak.EngelliKullaniminaUygunMu != engelliKullaniminaUygunMu)
                return false;
            if (durak.IlceAdi != jsonDurak.ILCEADI)
                return false;
            return true;


        }
    }
}