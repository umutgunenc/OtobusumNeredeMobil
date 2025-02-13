using Blazored.LocalStorage;
using Microsoft.Extensions.Logging;
using OtobusumNerede.Shared.Apis;
using Refit;
#if IOS
using Security;
#endif
#if ANDROID
using Xamarin.Android.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
#endif

namespace OtobusumNerede.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif
            // cache islemleri icin
            builder.Services.AddBlazoredLocalStorage();

            ConfigureRefit(builder.Services);

            return builder.Build();
        }
        static void ConfigureRefit(IServiceCollection services)
        {

            string apiBaseUrl = DeviceInfo.Platform == DevicePlatform.Android
                ? "https://10.0.2.2:7048"
                : "https://localhost:7048";
            //string apiBaseUrl = "https://10.0.2.2:7048";
            //string apiBaseUrl = "https://192.168.1.100:5000/";
            //string apiBaseUrl = "https://localhost:7048";


            services
                .AddRefitClient<IGetHatDurakApi>(GetRefitSettings)
                .ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri(apiBaseUrl));

            services
                .AddRefitClient<IGetHatlarApi>(GetRefitSettings)
                .ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri(apiBaseUrl));

            services
                .AddRefitClient<IGetSeferSaatleriApi> (GetRefitSettings)
                .ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri(apiBaseUrl));

            //string hatOtobusUrl = "https://api.ibb.gov.tr/iett/FiloDurum/SeferGerceklesme";

            services
                .AddRefitClient<IGetHatOtobusApi>(GetRefitSettings)
                .ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri(apiBaseUrl));

            services
                .AddRefitClient<IGetDuyurularApi>(GetRefitSettings)
                .ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri(apiBaseUrl));

            static RefitSettings GetRefitSettings(IServiceProvider sp)
            {
                return new RefitSettings
                {
                    HttpMessageHandlerFactory = () =>
                    {

#if ANDROID
                        var androidMessageHandler = new AndroidMessageHandler();
                        androidMessageHandler.ServerCertificateCustomValidationCallback = (HttpRequestMessage message, X509Certificate2? cert, X509Chain? chain, SslPolicyErrors sslPolicyErrors) => 
                        cert.Issuer == "CN=localhost" || sslPolicyErrors == SslPolicyErrors.None;

                        return androidMessageHandler;
#elif IOS
                        var nsUrlSessionHandler = new NSUrlSessionHandler
                        {
                            TrustOverrideForUrl = (NSUrlSessionHandler sender, string url, SecTrust trust) => url.StartsWith("https://localhost")
                        };
                        return nsUrlSessionHandler;
#endif
                        return null;

                    }
                };
            }

        }
    }
}
