
using OtobusumNerede.Shared.Services.Interfaces;

namespace OtobusumNerede.Shared.Services
{
    public class UyariService : IUyariServices
    {
        public async Task IzinUyarisiniGosterAsync(string mesaj)
        {
            bool goToSettings = await Application.Current.MainPage.DisplayAlert(
                    "Konum İzni Gerekli",mesaj,"Ayarları Aç", "Vazgeç");

            if (goToSettings)
                AppInfo.ShowSettingsUI();
        }

        public async Task GpsUyarisiniGosterAsync(string mesaj)
        {
            await Application.Current.MainPage.DisplayAlert("GPS Kapalı",mesaj, "Tamam");
        }
    }
}
