using OtobusumNerede.Shared.Services.Interfaces;

namespace OtobusumNerede.Shared.Services
{

    public class IzinService : IIzinServices
    {
        public async Task<bool> KonumIzinleriniKontrolEtAsync()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            switch (status)
            {
                case PermissionStatus.Unknown:
                    return false;
                case PermissionStatus.Denied:
                    return false;
                case PermissionStatus.Disabled:
                    return false;
                case PermissionStatus.Granted:
                    return true;
                case PermissionStatus.Restricted:
                    return false;
                case PermissionStatus.Limited:
                    return true;
                default:
                    return false;
            }

        }

        public async Task KonumIzniIsteAsync()
        {
            await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
        }

        //public async Task<bool> IzinVeridiMiAsync()
        //{
        //    if (DeviceInfo.Platform == DevicePlatform.iOS)
        //    {
        //        return true; // iOS'ta kullanıcı izin vermezse, genelde ayarlardan açmak gerekir.
        //    }
        //    else if (DeviceInfo.Platform == DevicePlatform.Android)
        //    {
        //        return !Permissions.ShouldShowRationale<Permissions.LocationWhenInUse>();
        //    }

        //    return false;
        //}
    }

    

}
