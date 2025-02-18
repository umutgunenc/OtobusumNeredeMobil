using System.Diagnostics;
using OtobusumNerede.Shared.ServicesDataModels;
using OtobusumNerede.Shared.Services.Interfaces;

namespace OtobusumNerede.Shared.Services
{
    public class KonumServices : IKonumServices
    {
        private readonly IIzinServices _izinService;
        private readonly IUyariServices _uyariService;
        private readonly ICihazServices _cihazService;


        public KonumServices(IIzinServices izinServices, IUyariServices uyariService, ICihazServices cihazService)
        {
            _izinService = izinServices;
            _uyariService = uyariService;
            _cihazService = cihazService;
        }

        public async Task<KonumResult> KonumBilgisiniAlAsync()
        {

            try
            {
                var status = await KonumIzinleriniKontrolEtAsync();
                if (status.konumIzniVerildimi && status.gpsAcikMi)
                {
                    var konumResult = KonumResult.IzinVerildi(await KonumuAlAsync());
                    return konumResult;

                }
                else if (status.konumIzniVerildimi && !status.gpsAcikMi)
                {
                    await _uyariService.GpsUyarisiniGosterAsync(status.mesaj);
                    return KonumResult.IzinVerilmedi(status.mesaj);
                }
                else
                {
                    await _uyariService.IzinUyarisiniGosterAsync(status.mesaj);
                    return KonumResult.IzinVerilmedi(status.mesaj);
                } 
            }
            catch (Exception ex )
            {
                Debug.WriteLine($"Konum alma hatası: {ex}");
                throw;
            }



            //try
            //{
            //    var status = await KonumIzinleriniKontrolEtAsync();
            //    if (!status.konumIzniVerildimi)
            //    {
            //        if (status.gpsAcikMi) { }
            //        else { 
            //        }

            //            await _uyariService.IzinUyarisiniGosterAsync(status.mesaj);
            //        return KonumResult.IzinVerilmedi(status.mesaj);
            //    }

            //    var location = await KonumuAlAsync();
            //    if (location == null)
            //        return KonumResult.IzinVerilmedi("Konum alınamadı.");
            //    else
            //        return KonumResult.IzinVerildi(location);
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine($"Konum alma hatası: {ex}");
            //    return KonumResult.IzinVerilmedi($"Hata: {ex.Message}");
            //}
        }

        private async Task<(bool konumIzniVerildimi, bool gpsAcikMi, string mesaj)> KonumIzinleriniKontrolEtAsync()
        {
            bool konumIzni = await _izinService.KonumIzinleriniKontrolEtAsync();
            bool gpsAcikMi = await _cihazService.GpsAcikMiAsync();
            string mesaj =null;

            if (konumIzni)
            {   
                if (!gpsAcikMi)
                {
                    mesaj = "Size daha iyi hizmet verebilmemiz için lütfen GPS'i açınız.";
                    return (konumIzni, gpsAcikMi, mesaj);
                }
                return (konumIzni, gpsAcikMi, mesaj);
            }
            else
            {
                await _izinService.KonumIzniIsteAsync();
                konumIzni = await _izinService.KonumIzinleriniKontrolEtAsync();

                if (konumIzni)           
                    return (konumIzni, gpsAcikMi, mesaj);                
                else
                {
                    mesaj = "Size daha iyi hizmet verebilmemiz için konum iznini ayarlardan etkinleştirin.";
                    return (konumIzni, gpsAcikMi, mesaj);
                }
            }

            //// Kullanıcıdan izin isteme


            //if (status != PermissionStatus.Granted)
            //{
            //    bool izinVerildiMi = await _izinService.IzinVeridiMiAsync();
            //    if (!izinVerildiMi)
            //        return (false, "Size daha iyi hizmet verebilmemiz için konum iznini ayarlardan etkinleştirin.");

            //}

            //// Kullanıcı izni verdi, GPS kontrolü yap
            //bool isGpsEnabledAfterGrant = await _cihazService.GpsAcikMiAsync();
            //if (!isGpsEnabledAfterGrant)
            //{
            //    await _uyariService.GpsUyarisiniGosterAsync();
            //    return (false, "Size daha iyi hizmet verebilmemiz için lütfen GPS'i açınız.");
            //}

            //return (true, null);
        }




        private async Task<Location> KonumuAlAsync()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(15));
            return await Geolocation.GetLocationAsync(request);
        }
    }




}