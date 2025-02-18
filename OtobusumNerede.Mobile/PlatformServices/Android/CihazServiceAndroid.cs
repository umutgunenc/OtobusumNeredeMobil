
#if ANDROID
using Android.Content;
using Android.Locations;
using OtobusumNerede.Shared.Services.Interfaces;

public class CihazServiceAndroid : ICihazServices
{
    public Task<bool> GpsAcikMiAsync()
    {
        if (Platform.CurrentActivity == null)
            return Task.FromResult(false);

        var locationManager = Platform.CurrentActivity.GetSystemService(Context.LocationService) as LocationManager;
        return Task.FromResult(locationManager?.IsProviderEnabled(LocationManager.GpsProvider) ?? false);
    }
}
#endif

