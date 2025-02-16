//using Blazored.LocalStorage;
//using Microsoft.Extensions.Logging;
//using OtobusumNerede.Shared.Apis;
//using OtobusumNerede.Shared.UIServices.Services;
//using OtobusumNerede.Shared.UIServices.Services.Interfaces;
//using Refit;
//#if IOS
//using Security;
//#endif
//#if ANDROID
//using Xamarin.Android.Net;
//using System.Net.Security;
//using System.Security.Cryptography.X509Certificates;
//#endif

//namespace OtobusumNerede.Mobile
//{
//    public static class MauiProgram
//    {
//        public static MauiApp CreateMauiApp()
//        {
//            var builder = MauiApp.CreateBuilder();
//            builder
//                .UseMauiApp<App>()
//                .ConfigureFonts(fonts =>
//                {
//                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
//                });

//            builder.Services.AddMauiBlazorWebView();

//#if DEBUG
//            builder.Services.AddBlazorWebViewDeveloperTools();
//            builder.Logging.AddDebug();
//#endif
//            // cache islemleri icin
//            builder.Services.AddBlazoredLocalStorage();

//            //builder.Services.AddScoped<IHatOtobusServices, HatOtobusServices>();
//            builder.Services.AddHttpClient<IHatOtobusServices, HatOtobusServices>()
//                .ConfigurePrimaryHttpMessageHandler(() =>
//                {
//#if ANDROID
//        var handler = new AndroidMessageHandler();
//        handler.ServerCertificateCustomValidationCallback = (msg, cert, chain, errors) => 
//            cert.Issuer == "CN=localhost" || errors == SslPolicyErrors.None;
//        return handler;
//#elif IOS
//                    var handler = new NSUrlSessionHandler();
//                    handler.TrustOverrideForUrl = (sender, url, trust) => url.StartsWith("https://api.ibb.gov.tr");
//                    return handler;
//#else
//        return new HttpClientHandler();
//#endif
//                });

//            builder.Services.AddBlazorBootstrap();

//            ConfigureRefit(builder.Services);

//            return builder.Build();
//        }
//        static void ConfigureRefit(IServiceCollection services)
//        {

//            string apiBaseUrl = DeviceInfo.Platform == DevicePlatform.Android
//                ? "https://10.0.2.2:7048"
//                : "https://localhost:7048";
//            //string apiBaseUrl = "https://10.0.2.2:7048";
//            //string apiBaseUrl = "https://otobusumnerede.runasp.net/";
//            //string apiBaseUrl = "https://localhost:7048";


//            services
//                .AddRefitClient<IGetHatDurakApi>(GetRefitSettings)
//                .ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri(apiBaseUrl));

//            services
//                .AddRefitClient<IGetHatlarApi>(GetRefitSettings)
//                .ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri(apiBaseUrl));

//            services
//                .AddRefitClient<IGetSeferSaatleriApi> (GetRefitSettings)
//                .ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri(apiBaseUrl));

//            services
//                .AddRefitClient<IGetHatOtobusApi>(GetRefitSettings)
//                .ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri(apiBaseUrl));

//            services
//                .AddRefitClient<IGetDuyurularApi>(GetRefitSettings)
//                .ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri(apiBaseUrl));

//            static RefitSettings GetRefitSettings(IServiceProvider sp)
//            {
//                return new RefitSettings
//                {
//                    HttpMessageHandlerFactory = () =>
//                    {

//#if ANDROID
//                        var androidMessageHandler = new AndroidMessageHandler();
//                        androidMessageHandler.ServerCertificateCustomValidationCallback = (HttpRequestMessage message, X509Certificate2? cert, X509Chain? chain, SslPolicyErrors sslPolicyErrors) => 
//                        cert.Issuer == "CN=localhost" || sslPolicyErrors == SslPolicyErrors.None;

//                        return androidMessageHandler;
//#elif IOS
//                        var nsUrlSessionHandler = new NSUrlSessionHandler
//                        {
//                            TrustOverrideForUrl = (NSUrlSessionHandler sender, string url, SecTrust trust) => url.StartsWith("https://localhost")
//                        };
//                        return nsUrlSessionHandler;
//#endif
//                        return null;

//                    }
//                };
//            }

//        }
//    }
//}


using Blazored.LocalStorage;
using Microsoft.Extensions.Logging;
using OtobusumNerede.Shared.Apis;
using OtobusumNerede.Shared.UIServices.Services;
using OtobusumNerede.Shared.UIServices.Services.Interfaces;
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

            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddHttpClient<IHatOtobusServices, HatOtobusServices>()
                .ConfigurePrimaryHttpMessageHandler(() => CreatePlatformMessageHandler());

            builder.Services.AddBlazorBootstrap();

            ConfigureRefit(builder.Services);

            return builder.Build();
        }

        static void ConfigureRefit(IServiceCollection services)
        {
            string apiBaseUrl = GetBaseUrl();

            services
                .AddRefitClient<IGetHatDurakApi>(GetRefitSettings)
                .ConfigureHttpClient(ConfigureHttpClient);

            services
                .AddRefitClient<IGetHatlarApi>(GetRefitSettings)
                .ConfigureHttpClient(ConfigureHttpClient);

            services
                .AddRefitClient<IGetSeferSaatleriApi>(GetRefitSettings)
                .ConfigureHttpClient(ConfigureHttpClient);

            services
                .AddRefitClient<IGetHatOtobusApi>(GetRefitSettings)
                .ConfigureHttpClient(ConfigureHttpClient);

            services
                .AddRefitClient<IGetDuyurularApi>(GetRefitSettings)
                .ConfigureHttpClient(ConfigureHttpClient);

            static void ConfigureHttpClient(HttpClient client)
            {
                client.BaseAddress = new Uri(GetBaseUrl());
                client.Timeout = TimeSpan.FromSeconds(30);
            }

            static string GetBaseUrl()
            {
#if DEBUG
                return DeviceInfo.Platform == DevicePlatform.Android
                    ? "https://10.0.2.2:7048"
                    : "https://localhost:7048";
#else
                return "https://otobusumnerede.runasp.net/"; // Production URL
#endif
            }
        }

        static RefitSettings GetRefitSettings(IServiceProvider sp)
        {
            return new RefitSettings
            {
                HttpMessageHandlerFactory = () => CreatePlatformMessageHandler()
            };
        }

        static HttpMessageHandler CreatePlatformMessageHandler()
        {
#if ANDROID
            var handler = new AndroidMessageHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
#if DEBUG
                return true; // DEBUG modda tüm sertifikaları kabul et
#else
                // PRODUCTION için sertifika kontrolü
                return cert?.Issuer == "CN=Let's Encrypt Authority X3" || 
                       errors == SslPolicyErrors.None;
#endif
            };
            return handler;
#elif IOS
            var handler = new NSUrlSessionHandler();
#if DEBUG
            handler.TrustOverrideForUrl = (sender, url, trust) => true;
#else
            handler.TrustOverrideForUrl = (sender, url, trust) => 
                url.StartsWith("https://otobusumnerede.runasp.net");
#endif
            return handler;
#else
            return new HttpClientHandler();
#endif
        }
    }
}
