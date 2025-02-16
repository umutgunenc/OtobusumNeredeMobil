using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.ApplicationModel;
using OtobusumNerede.Shared.ServicesDataModels;

namespace OtobusumNerede.Shared.Services
{
    public class LocationServices : ILocationServices
    {
        public async Task<LocationResult> KonumBilgisiniAlAsync()
        {
            try
            {
                var status = await CheckAndRequestLocationPermission();
                if (!status.IsGranted)
                    return LocationResult.Fail(status.ErrorMessage);

                var location = await KonumBilgisiniAlAsyncPrivate();
                if (location == null)
                    return LocationResult.Fail("Konum alınamadı.");
                else
                    return LocationResult.Success(location);
            }
            catch (Exception ex)
            {
                return LocationResult.Fail($"Hata: {ex.Message}");
            }
        }

        private async Task<(bool IsGranted, string ErrorMessage)> CheckAndRequestLocationPermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if (status == PermissionStatus.Granted)
                return (true, null);
            //TODO izin red edildi ve gps ile ilgili bir kontrol ekle
            status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            return (status == PermissionStatus.Granted, "Konum izni reddedildi.");
        }

        private async Task<Location> KonumBilgisiniAlAsyncPrivate()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(15));
            return await Geolocation.GetLocationAsync(request);
        }


    }


}