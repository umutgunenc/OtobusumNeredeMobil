

namespace OtobusumNerede.Mobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await CheckAndRequestLocationPermission();
        }

        private async Task CheckAndRequestLocationPermission()
        {
            try
            {
                // İzin durumunu kontrol et
                var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

                if (status == PermissionStatus.Granted)
                    //TODO Konum Bilgisini Al
                    await DisplayAlert("Bilgi", "Konum izni zaten verilmiş!", "Tamam");
                else
                    await RequestLocationPermission();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", ex.Message, "Tamam");
            }
        }

        private async Task RequestLocationPermission()
        {
            var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

            if (status != PermissionStatus.Granted)
            {
                // İzin reddedildi, kullanıcıyı yönlendir
                bool retry = await DisplayAlert(
                    "Uyarı",
                    "Size daha iyi hizmet verebilmek için konum iznine ihtiyacımız var\nKonum izni vermek ister misiniz ?",
                    "Ayarlara Git",
                    "İptal"
                );

                if (retry)
                    AppInfo.ShowSettingsUI();
            }
        }
    }
}


